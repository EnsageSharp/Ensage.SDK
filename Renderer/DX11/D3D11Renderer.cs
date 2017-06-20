// <copyright file="D3D11Renderer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX11
{
    using System;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Renderer.Metadata;

    using SharpDX;
    using SharpDX.Direct2D1;
    using SharpDX.DirectWrite;

    using Color = System.Drawing.Color;

    [ExportRenderer(RenderMode.Dx11)]
    public class D3D11Renderer : IRenderer
    {
        private readonly BrushCache brushCache;

        private readonly ID3D11Context context;

        private readonly TextFormatCache textFormatCache;

        [ImportingConstructor]
        public D3D11Renderer([Import] ID3D11Context context, [Import] BrushCache brushCache, [Import] TextFormatCache textFormatCache)
        {
            this.context = context;

            this.brushCache = brushCache;
            this.textFormatCache = textFormatCache;
        }

        public event EventHandler Draw
        {
            add
            {
                this.context.Draw += value;
            }

            remove
            {
                this.context.Draw -= value;
            }
        }

        public void DrawCircle(Vector2 center, float radius, Color color)
        {
            this.context.RenderTarget.DrawEllipse(new Ellipse(center, radius, radius), this.brushCache.GetOrCreate(color));
        }

        public void DrawLine(Vector2 start, Vector2 end, Color color, float width = 1.0f)
        {
            this.context.RenderTarget.StrokeWidth = width;
            this.context.RenderTarget.DrawLine(start, end, this.brushCache.GetOrCreate(color));
        }

        public void DrawRectangle(RectangleF rect, Color color, float width = 1.0f)
        {
            this.context.RenderTarget.StrokeWidth = width;
            this.context.RenderTarget.DrawRectangle(rect, this.brushCache.GetOrCreate(color));
        }

        public void DrawText(Vector2 position, string text, Color color, string fontFamily = "Arial", float fontSize = 16)
        {
            var font = this.textFormatCache.GetOrCreate(fontFamily, fontSize);
            var brush = this.brushCache.GetOrCreate(color);

            using (var layout = new TextLayout(this.context.DirectWrite, text, font, float.MaxValue, float.MaxValue))
            {
                this.context.RenderTarget.DrawTextLayout(position, layout, brush);
            }
        }
    }
}