// <copyright file="D3D11TextureManager.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
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
    using Ensage.SDK.VPK.Content;

    using PlaySharp.Toolkit.Helper.Annotations;

    using NLog;

    using SharpDX;
    using SharpDX.WIC;

    using Bitmap = SharpDX.Direct2D1.Bitmap;

    [Export]
    [ExportTextureManager(RenderMode.Dx11)]
    public sealed class D3D11TextureManager : ITextureManager
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly ImagingFactory imagingFactory;

        private readonly ID3D11Context renderContext;

        private readonly Dictionary<string, D3D11Texture> textureCache = new Dictionary<string, D3D11Texture>();

        private readonly VpkBrowser vpkBrowser;

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

                Log.Error(new Exception($"Can't find Dota Texture: {file}"));

                bitmapStream = this.vpkBrowser.FindImage(@"panorama\images\spellicons\invoker_empty1_png.vtex_c");
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
                return false;
            }
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

                using (var bitmapDecoder = new BitmapDecoder(this.imagingFactory, stream, DecodeOptions.CacheOnDemand))
                {
                    var frame = bitmapDecoder.GetFrame(0);
                    using (var converter = new FormatConverter(this.imagingFactory))
                    {
                        converter.Initialize(frame, PixelFormat.Format32bppPRGBA); // Format32bppPRGBA
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
                this.imagingFactory.Dispose();
            }

            this.disposed = true;
        }
    }
}