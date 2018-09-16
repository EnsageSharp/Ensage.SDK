// <copyright file="RendererManager.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Renderer.Metadata;

    using NLog;

    using SharpDX;

    using Color = System.Drawing.Color;

    [Export(typeof(IRendererManager))]
    public sealed class RendererManager : IRendererManager
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly IRenderer active;

        private bool disposed;

        [ImportingConstructor]
        public RendererManager([ImportMany] IEnumerable<Lazy<IRenderer, IRendererMetadata>> renderers)
        {
            this.active = renderers.First(e => e.Metadata.Mode == Drawing.RenderMode).Value;
        }

        public event EventHandler Draw
        {
            add
            {
                this.active.Draw += value;
            }

            remove
            {
                this.active.Draw -= value;
            }
        }

        public ITextureManager TextureManager
        {
            get
            {
                return this.active.TextureManager;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void DrawCircle(Vector2 center, float radius, Color color, float width = 1.0f)
        {
            try
            {
                this.active.DrawCircle(center, radius, color, width);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public void DrawFilledRectangle(RectangleF rect, Color color, Color backgroundColor, float borderWidth = 1.0f)
        {
            try
            {
                this.active.DrawFilledRectangle(rect, color, backgroundColor, borderWidth);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public void DrawLine(Vector2 start, Vector2 end, Color color, float width = 1.0f)
        {
            try
            {
                this.active.DrawLine(start, end, color, width);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public void DrawRectangle(RectangleF rect, Color color, float width = 1.0f)
        {
            try
            {
                this.active.DrawRectangle(rect, color, width);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public void DrawText(Vector2 position, string text, Color color, float fontSize = 13f, string fontFamily = "Calibri")
        {
            try
            {
                this.active.DrawText(position, text, color, fontSize, fontFamily);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public void DrawText(RectangleF position, string text, Color color, RendererFontFlags flags = RendererFontFlags.Left, float fontSize = 13f, string fontFamily = "Calibri")
        {
            try
            {
                this.active.DrawText(position, text, color, flags, fontSize, fontFamily);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public void DrawTexture(string textureKey, RectangleF rect, float rotation = 0, float opacity = 1)
        {
            try
            {
                this.active.DrawTexture(textureKey, rect, rotation, opacity);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public Vector2 GetTextureSize(string textureKey)
        {
            try
            {
                return this.active.TextureManager.GetTextureSize(textureKey);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return Vector2.Zero;
            }
        }

        public Vector2 MeasureText(string text, float fontSize = 13, string fontFamily = "Calibri")
        {
            try
            {
                return this.active.MeasureText(text, fontSize, fontFamily);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return Vector2.Zero;
            }
        }

        [Obsolete("Use MeasureText")]
        public Vector2 MessureText(string text, float fontSize = 13, string fontFamily = "Calibri")
        {
            return this.MeasureText(text, fontSize, fontFamily);
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.active.Dispose();
            }

            this.disposed = true;
        }
    }
}