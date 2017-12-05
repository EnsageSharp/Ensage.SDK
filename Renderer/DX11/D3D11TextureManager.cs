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

    using Ensage.SDK.Renderer.Metadata;
    using Ensage.SDK.VPK;

    using log4net;

    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    using SharpDX.IO;
    using SharpDX.WIC;

    using Bitmap = SharpDX.Direct2D1.Bitmap;

    [Export]
    [ExportTextureManager(RenderMode.Dx11)]
    public sealed class D3D11TextureManager : ITextureManager
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ImagingFactory imagingFactory;

        private readonly ID3D11Context renderContext;

        private readonly VpkBrowser vpkBrowser;

        private readonly Dictionary<string, D3D11Texture> textureCache = new Dictionary<string, D3D11Texture>();

        private bool disposed;

        [ImportingConstructor]
        public D3D11TextureManager([Import] ID3D11Context renderContext, [Import] VpkBrowser vpkBrowser)
        {
            this.renderContext = renderContext;
            this.vpkBrowser = vpkBrowser;

            this.imagingFactory = new ImagingFactory();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [CanBeNull]
        public D3D11Texture GetTexture(string textureKey)
        {
            if (this.textureCache.TryGetValue(textureKey, out var result))
            {
                return result;
            }

            return null;
        }

        public bool LoadFromFile(string textureKey, string file)
        {
            try
            {
                if (this.textureCache.ContainsKey(textureKey))
                {
                    return true;
                }

                var texture = this.textureCache.Values.FirstOrDefault(x => x.File == file);
                if (texture != null)
                {
                    this.textureCache[textureKey] = texture;
                    return true;
                }

                using (var fileStream = new NativeFileStream(file, NativeFileMode.Open, NativeFileAccess.Read))
                {
                    var result = this.LoadFromStream(textureKey, fileStream);
                    if (result)
                    {
                        this.textureCache[textureKey].File = file;
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

        public bool LoadFromMemory(string textureKey, byte[] data)
        {
            try
            {
                if (this.textureCache.ContainsKey(textureKey))
                {
                    return true;
                }

                using (var memoryStream = new MemoryStream(data))
                {
                    return this.LoadFromStream(textureKey, memoryStream);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        public bool LoadFromStream(string textureKey, Stream stream)
        {
            try
            {
                if (this.textureCache.ContainsKey(textureKey))
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
                        this.textureCache[textureKey] = new D3D11Texture(bitmap);
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

        public bool LoadFromDota(string textureKey, string file)
        {
            try
            {
                var bitmapStream = this.vpkBrowser.FindImage(file);
                if (bitmapStream != null)
                {
                    return this.LoadFromStream(textureKey, bitmapStream);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            return false;
        }

        public bool LoadFromResource(string textureKey, string file, Assembly assembly = null)
        {
            if (this.textureCache.ContainsKey(textureKey))
            {
                return true;
            }

            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (assembly == null)
            {
                assembly = Assembly.GetCallingAssembly();
            }

            var resourceFile = assembly.GetManifestResourceNames().FirstOrDefault(f => f.EndsWith(file));
            if (resourceFile == null)
            {
                Log.Warn($"Not found {assembly.GetName().Name} - {file}");

                foreach (var resourceName in assembly.GetManifestResourceNames())
                {
                    Log.Debug($"candidate {resourceName}");
                }

                throw new ArgumentNullException(nameof(resourceFile));
            }

            using (var ms = new MemoryStream())
            {
                assembly.GetManifestResourceStream(resourceFile)?.CopyTo(ms);
                return this.LoadFromStream(textureKey, ms);
            }
        }

        private void Dispose(bool disposing)
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