// <copyright file="RendererManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
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

    [Export(typeof(IRendererManager))]
    public sealed class RendererManager : IRendererManager
    {
        private readonly IRenderer active;

        private bool disposed;

        [ImportingConstructor]
        public RendererManager(
            [ImportMany] IEnumerable<Lazy<IRenderer, IRendererMetadata>> renderers,
            [ImportMany] IEnumerable<Lazy<ITextureManager, IRendererMetadata>> textureManagers)
        {
            this.active = renderers.First(e => e.Metadata.Mode == Drawing.RenderMode).Value;
            this.TextureManager = textureManagers.First(e => e.Metadata.Mode == Drawing.RenderMode).Value;
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

        public ITextureManager TextureManager { get; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void DrawTexture(string textureKey, RectangleF rect, float rotation = 0, float opacity = 1)
        {
            this.active.DrawTexture(textureKey, rect, rotation, opacity);
        }

        public Vector2 MessureText(string text, float fontSize = 13, string fontFamily = "Calibri")
        {
            return active.MessureText(text, fontSize, fontFamily);
        }

        public void DrawCircle(Vector2 center, float radius, Color color, float width = 1.0f)
        {
            this.active.DrawCircle(center, radius, color, width);
        }

        public void DrawLine(Vector2 start, Vector2 end, Color color, float width = 1.0f)
        {
            this.active.DrawLine(start, end, color, width);
        }

        public void DrawRectangle(RectangleF rect, Color color, float width = 1.0f)
        {
            this.active.DrawRectangle(rect, color, width);
        }

        public void DrawText(Vector2 position, string text, Color color, float fontSize = 13f, string fontFamily = "Calibri")
        {
            this.active.DrawText(position, text, color, fontSize, fontFamily);
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