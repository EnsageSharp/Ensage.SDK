// <copyright file="D3D9TextureManager.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
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
    using Ensage.SDK.VPK;
    using Ensage.SDK.VPK.Content;

    using PlaySharp.Toolkit.Helper.Annotations;

    using NLog;

    using SharpDX;
    using SharpDX.Direct3D9;

    [Export]
    [ExportTextureManager(RenderMode.Dx9)]
    public sealed class D3D9TextureManager : ITextureManager
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly ID3D9Context renderContext;

        private readonly Dictionary<string, D3D9Texture> textureCache = new Dictionary<string, D3D9Texture>();

        private readonly VpkBrowser vpkBrowser;

        private bool disposed;

        [ImportingConstructor]
        public D3D9TextureManager([Import] ID3D9Context renderContext, [Import] VpkBrowser vpkBrowser)
        {
            this.renderContext = renderContext;
            this.vpkBrowser = vpkBrowser;
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

        public Vector2 GetTextureSize(string textureKey)
        {
            if (!this.textureCache.TryGetValue(textureKey, out var texture))
            {
                return Vector2.Zero;
            }

            return new Vector2(texture.Bitmap.Size.Width, texture.Bitmap.Size.Height);
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

                throw new Exception($"Can't find Dota Texture: {file}");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            return false;
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
                    var e = Path.GetExtension(file);
                    switch (e)
                    {
                        case ".png":
                            {
                                var result = this.LoadFromStream(textureKey, fileStream);
                                if (result)
                                {
                                    this.textureCache[textureKey].File = file;
                                    return true;
                                }
                            }
                            break;

                        case ".vtex_c":
                            {
                                var resourceFile = new ResourceFile(fileStream);
                                var vtex = resourceFile.ResourceEntries.OfType<VTex>().FirstOrDefault();
                                if (vtex != null)
                                {
                                    var result = this.LoadFromStream(textureKey, vtex.DataStream);
                                    if (result)
                                    {
                                        this.textureCache[textureKey].File = file;
                                        return true;
                                    }
                                }
                            }
                            break;
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

                this.vpkBrowser.Dispose();
            }

            this.disposed = true;
        }
    }
}