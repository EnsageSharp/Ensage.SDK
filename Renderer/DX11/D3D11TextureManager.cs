// <copyright file="D3D11TextureManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX11
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Extensions;
    using Ensage.SDK.Renderer.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    using SharpDX.IO;
    using SharpDX.WIC;

    using Bitmap = SharpDX.Direct2D1.Bitmap;

    [Export]
    [ExportTextureManager(RenderMode.Dx11)]
    public class D3D11TextureManager : ITextureManager
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ImagingFactory imagingFactory;

        private readonly ID3D11Context renderContext;

        private readonly Dictionary<string, D3D11Texture> textureCache = new Dictionary<string, D3D11Texture>();

        private bool disposed;

        [ImportingConstructor]
        public D3D11TextureManager([Import] ID3D11Context renderContext)
        {
            this.renderContext = renderContext;

            this.imagingFactory = new ImagingFactory();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [CanBeNull]
        public Bitmap GetBitmap(string bitmapKey)
        {
            D3D11Texture result;
            if (this.textureCache.TryGetValue(bitmapKey, out result))
            {
                return result.Bitmap;
            }

            return null;
        }

        public bool LoadFromFile(string bitmapKey, string file)
        {
            try
            {
                if (this.textureCache.ContainsKey(bitmapKey))
                {
                    return true;
                }

                var texture = this.textureCache.Values.FirstOrDefault(x => x.File == file);
                if (texture != null)
                {
                    this.textureCache[bitmapKey] = texture;
                    return true;
                }

                using (var fileStream = new NativeFileStream(file, NativeFileMode.Open, NativeFileAccess.Read))
                {
                    var result = this.LoadFromStream(bitmapKey, fileStream);
                    if (result)
                    {
                        this.textureCache[bitmapKey].File = file;
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        public bool LoadFromMemory(string bitmapKey, byte[] data)
        {
            try
            {
                if (this.textureCache.ContainsKey(bitmapKey))
                {
                    return true;
                }

                using (var memoryStream = new MemoryStream(data))
                {
                    return this.LoadFromStream(bitmapKey, memoryStream);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        public bool LoadFromStream(string bitmapKey, Stream stream)
        {
            try
            {
                if (this.textureCache.ContainsKey(bitmapKey))
                {
                    return true;
                }

                using (var bitmapDecoder = new BitmapDecoder(this.imagingFactory, stream, DecodeOptions.CacheOnDemand))
                {
                    var frame = bitmapDecoder.GetFrame(0);
                    using (var converter = new FormatConverter(this.imagingFactory))
                    {
                        converter.Initialize(frame, PixelFormat.Format32bppPRGBA);
                        var bitmap = Bitmap.FromWicBitmap(this.renderContext.RenderTarget, converter);
                        this.textureCache[bitmapKey] = new D3D11Texture(bitmap);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (var texture in this.textureCache.Values)
                {
                    texture.Dispose();
                }

                this.textureCache.Clear();

                this.imagingFactory.Dispose();
            }

            this.disposed = true;
        }
    }
}