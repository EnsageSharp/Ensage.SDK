// <copyright file="D3TextureManager.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer.Texture;
    using Ensage.SDK.Renderer.Utils;
    using Ensage.SDK.Renderer.VPK;
    using Ensage.SDK.Renderer.VPK.Resources;

    using NLog;

    using SharpDX;

    public abstract class D3TextureManager<T> : ITextureManager
        where T : D3Texture
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly HashSet<string> loadedTextures = new HashSet<string>();

        private readonly TextureRoundRatio textureRoundRatio = new TextureRoundRatio();

        private readonly VpkBrowser vpkBrowser = new VpkBrowser();

        private readonly Task vpkReaderTask;

        private bool disposed;

        protected D3TextureManager()
        {
            this.vpkReaderTask = Task.Run(() => this.vpkBrowser.ReadFiles());

            this.vpkReaderTask.ContinueWith(
                task =>
                    {
                        if (!task.IsCompleted || task.IsFaulted)
                        {
                            Log.Error("VPK reader failed!");
                            return;
                        }

                        UpdateManager.BeginInvoke(() => this.VpkLoaded?.Invoke(this, EventArgs.Empty), 500); // sync?
                    });

            this.LoadAbilityFromDota(AbilityId.invoker_empty1);
        }

        internal event EventHandler VpkLoaded;

        protected IDictionary<string, T> TextureCache { get; } = new ConcurrentDictionary<string, T>();

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public T GetTexture(string textureKey)
        {
            if (this.TextureCache.TryGetValue(textureKey, out var result))
            {
                return result;
            }

            return this.TextureCache[nameof(AbilityId.invoker_empty1)];
        }

        public Vector2 GetTextureSize(string textureKey)
        {
            if (this.TextureCache.TryGetValue(textureKey, out var result))
            {
                return result.Size;
            }

            return Vector2.Zero;
        }

        public void LoadAbilityFromDota(string abilityName, bool rounded = false)
        {
            var key = abilityName;
            var isItem = abilityName.StartsWith("item_");
            var textureFolder = isItem ? "items" : "spellicons";
            var textureName = isItem ? key.Substring("item_".Length) : key;
            var file = $@"panorama\images\{textureFolder}\{textureName}_png.vtex_c";

            if (rounded)
            {
                key += "_rounded";
            }

            this.LoadFromDota(
                key,
                file,
                new TextureProperties
                {
                    Rounded = rounded
                });
        }

        public void LoadAbilityFromDota(AbilityId abilityId, bool rounded = false)
        {
            this.LoadAbilityFromDota(abilityId.ToString(), rounded);
        }

        public void LoadFromDota(string textureKey, string file, TextureProperties properties = default)
        {
            if (!this.loadedTextures.Add(textureKey))
            {
                return;
            }

            Task.Run(
                async () =>
                    {
                        try
                        {
                            await this.vpkReaderTask.ConfigureAwait(false);

                            var bitmap = this.vpkBrowser.GetBitmap(file);
                            if (bitmap == null)
                            {
                                Log.Warn("Texture file not found: " + file);
                                return;
                            }

                            if (properties.Sliced)
                            {
                                for (var i = 0; i <= 100; i++)
                                {
                                    this.LoadFromBitmap(textureKey + i, bitmap.Pie(i), properties);
                                }
                            }
                            else
                            {
                                this.LoadFromBitmap(textureKey, bitmap, properties);
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Error(e);
                        }
                    });
        }

        public void LoadFromFile(string textureKey, string file, TextureProperties properties = default)
        {
            if (!this.loadedTextures.Add(textureKey))
            {
                return;
            }

            Task.Run(
                () =>
                    {
                        try
                        {
                            if (!File.Exists(file))
                            {
                                Log.Warn("Texture file not found: " + file);
                                return;
                            }

                            using (var fileStream = File.OpenRead(file))
                            {
                                var resource = new ResourceFile(fileStream, Path.GetExtension(file));
                                var bitmap = resource.GetBitmap();

                                this.LoadFromBitmap(textureKey, bitmap, properties);
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Error(e);
                        }
                    });
        }

        public void LoadFromResource(string textureKey, string file, TextureProperties properties = default)
        {
            if (!this.loadedTextures.Add(textureKey))
            {
                return;
            }

            var assembly = Assembly.GetCallingAssembly();

            Task.Run(
                () =>
                    {
                        try
                        {
                            var resourceFile = Array.Find(assembly.GetManifestResourceNames(), x => x.EndsWith(file));
                            if (resourceFile == null)
                            {
                                Log.Warn("Texture file not found: " + file);
                                return;
                            }

                            var resourceStream = assembly.GetManifestResourceStream(resourceFile);
                            if (resourceStream == null)
                            {
                                return;
                            }

                            using (resourceStream)
                            {
                                this.LoadFromBitmap(textureKey, new Bitmap(resourceStream), properties);
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Error(e);
                        }
                    });
        }

        public void LoadHeroFromDota(HeroId heroId, bool rounded = false, bool icon = false)
        {
            this.LoadHeroFromDota(heroId.ToString(), rounded, icon);
        }

        public void LoadHeroFromDota(string heroName, bool rounded = false, bool icon = false)
        {
            if (icon)
            {
                var file = $@"panorama\images\heroes\icons\{heroName}_png.vtex_c";
                this.LoadFromDota(heroName + "_icon", file);
            }

            this.LoadUnitFromDota(heroName, rounded);
        }

        public void LoadUnitFromDota(string unitName, bool rounded = false)
        {
            var key = unitName;
            var file = $@"panorama\images\heroes\{unitName}_png.vtex_c";

            if (rounded)
            {
                key += "_rounded";
            }

            this.LoadFromDota(
                key,
                file,
                new TextureProperties
                {
                    Rounded = rounded
                });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (var texture in this.TextureCache.Values)
                {
                    texture?.Dispose();
                }

                this.loadedTextures.Clear();
                this.TextureCache.Clear();
            }

            this.disposed = true;
        }

        protected abstract void LoadFromStream(string textureKey, Stream stream);

        private void LoadFromBitmap(string textureKey, Bitmap bitmap, TextureProperties properties = default)
        {
            using (var stream = new MemoryStream())
            {
                if (properties.Rounded)
                {
                    bitmap = bitmap.Round(this.textureRoundRatio.GetRatio(textureKey));
                }

                if (!properties.ColorRatio.IsZero)
                {
                    bitmap = bitmap.AdjustColor(properties.ColorRatio);
                }

                if (properties.Brightness != 0)
                {
                    bitmap = bitmap.AdjustBrightness(properties.Brightness);
                }

                bitmap.Save(stream, ImageFormat.Png);
                bitmap.Dispose();

                this.LoadFromStream(textureKey, stream);
            }
        }
    }
}