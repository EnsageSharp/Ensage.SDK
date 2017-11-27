// <copyright file="D3D9TextureManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX9
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Renderer.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    using SharpDX.Direct3D9;

    [Export]
    [ExportTextureManager(RenderMode.Dx9)]
    public sealed class D3D9TextureManager : ITextureManager
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ImageConverter imageConverter = new ImageConverter();

        private readonly ID3D9Context renderContext;

        private readonly Dictionary<string, D3D9Texture> textureCache = new Dictionary<string, D3D9Texture>();

        private bool disposed;

        [ImportingConstructor]
        public D3D9TextureManager([Import] ID3D9Context renderContext)
        {
            this.renderContext = renderContext;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [CanBeNull]
        public D3D9Texture GetTexture(string textureKey)
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

                var cacheEntry = this.textureCache.Values.FirstOrDefault(x => x.File == file);
                if (cacheEntry != null)
                {
                    this.textureCache[textureKey] = cacheEntry;
                    return true;
                }

                if (!File.Exists(file))
                {
                    throw new FileNotFoundException(file);
                }

                using (var fileStream = File.OpenRead(file))
                {
                    var result = this.LoadFromStream(textureKey, fileStream);
                    if (result)
                    {
                        this.textureCache[textureKey].File = file;
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            return false;
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
            }

            return false;
        }

        public bool LoadFromStream(string textureKey, Stream stream)
        {
            try
            {
                if (this.textureCache.ContainsKey(textureKey))
                {
                    return true;
                }

                var bitmap = (Bitmap)Image.FromStream(stream);
                var bitmapData = (byte[])new ImageConverter().ConvertTo(bitmap, typeof(byte[]));
                var texture = Texture.FromMemory(
                    this.renderContext.Device,
                    bitmapData,
                    bitmap.Width,
                    bitmap.Height,
                    0,
                    Usage.None,
                    Format.A1,
                    Pool.Default,
                    Filter.Default,
                    Filter.Default,
                    0);

                if (texture != null)
                {
                    this.textureCache[textureKey] = new D3D9Texture(texture, bitmap);
                    return true;
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            return false;
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
            }

            this.disposed = true;
        }
    }
}