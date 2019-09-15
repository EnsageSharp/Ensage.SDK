// <copyright file="RenderManager.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Renderer.Metadata;

    using SharpDX;

    using Color = System.Drawing.Color;

    [Export(typeof(IRenderManager))]
    public sealed class RenderManager : IRenderManager
    {
        private readonly IRenderer active;

        private bool disposed;

        [ImportingConstructor]
        public RenderManager([ImportMany] IEnumerable<Lazy<IRenderer, IRendererMetadata>> renderers)
        {
            this.active = renderers.First(e => e.Metadata.Mode == Drawing.RenderMode).Value;
        }

        public delegate void EventHandler(IRenderer renderer);

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

        public void DrawCircle(Vector2 center, float radius, Color color, float width = 1)
        {
            this.active.DrawCircle(center, radius, color, width);
        }

        public void DrawFilledRectangle(RectangleF rect, Color color, Color borderColor, float borderWidth = 1)
        {
            this.active.DrawFilledRectangle(rect, color, borderColor, borderWidth);
        }

        public void DrawFilledRectangle(RectangleF rect, Color color)
        {
            this.active.DrawFilledRectangle(rect, color);
        }

        public void DrawLine(Vector2 start, Vector2 end, Color color, float width = 1)
        {
            this.active.DrawLine(start, end, color, width);
        }

        public void DrawRectangle(RectangleF rect, Color color, float borderWidth = 1)
        {
            this.active.DrawRectangle(rect, color, borderWidth);
        }

        public void DrawText(Vector2 position, string text, Color color, float fontSize = 13, string fontFamily = "Calibri")
        {
            this.active.DrawText(position, text, color, fontSize, fontFamily);
        }

        public void DrawText(RectangleF position, string text, Color color, RendererFontFlags flags = RendererFontFlags.Left, float fontSize = 13, string fontFamily = "Calibri")
        {
            this.active.DrawText(position, text, color, flags, fontSize, fontFamily);
        }

        public void DrawTexture(string textureKey, Vector2 position, Vector2 size, float rotation = 0, float opacity = 1)
        {
            this.active.DrawTexture(textureKey, position, size, rotation, opacity);
        }

        public void DrawTexture(string textureKey, RectangleF rect, float rotation = 0, float opacity = 1)
        {
            this.active.DrawTexture(textureKey, rect, rotation, opacity);
        }

        public Vector2 GetTextureSize(string textureKey)
        {
            return this.active.GetTextureSize(textureKey);
        }

        public Vector2 MeasureText(string text, float fontSize = 13, string fontFamily = "Calibri")
        {
            return this.active.MeasureText(text, fontSize, fontFamily);
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