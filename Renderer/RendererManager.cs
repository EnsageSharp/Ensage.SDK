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
    public class RendererManager : IRendererManager
    {
        private readonly IRenderer active;

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

        public void DrawCircle(Vector2 center, float radius, Color color)
        {
            this.active.DrawCircle(center, radius, color);
        }

        public void DrawLine(Vector2 start, Vector2 end, Color color, float width = 1.0f)
        {
            this.active.DrawLine(start, end, color, width);
        }

        public void DrawRectangle(RectangleF rect, Color color, float width = 1.0f)
        {
            this.active.DrawRectangle(rect, color, width);
        }

        public void DrawText(Vector2 position, string text, Color color, string fontFamily = "Calibri", float fontSize = 13f)
        {
            this.active.DrawText(position, text, color, fontFamily, fontSize);
        }
    }
}