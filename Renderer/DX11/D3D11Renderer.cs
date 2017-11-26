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
    public sealed class D3D11Renderer : IRenderer
    {
        private readonly BrushCache brushCache;

        private readonly ID3D11Context context;

        private readonly TextFormatCache textFormatCache;

        private readonly D3D11TextureManager textureManager;

        private bool disposed;

        [ImportingConstructor]
        public D3D11Renderer([Import] ID3D11Context context, [Import] BrushCache brushCache, [Import] TextFormatCache textFormatCache, [Import] D3D11TextureManager textureManager)
        {
            this.context = context;

            this.brushCache = brushCache;
            this.textFormatCache = textFormatCache;
            this.textureManager = textureManager;
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

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void DrawBitmap(string bitmapKey, RectangleF rect, float rotation = 0.0f, float opacity = 1.0f)
        {
            var bitmap = this.textureManager.GetBitmap(bitmapKey);
            if (bitmap == null)
            {
                throw new BitmapNotFoundException(bitmapKey);
            }

            this.context.RenderTarget.DrawBitmap(bitmap, rect, opacity, BitmapInterpolationMode.Linear);
        }

        public void DrawCircle(Vector2 center, float radius, Color color, float width = 1.0f)
        {
            this.context.RenderTarget.DrawEllipse(new Ellipse(center, radius, radius), this.brushCache.GetOrCreate(color), width);
        }

        public void DrawLine(Vector2 start, Vector2 end, Color color, float width = 1.0f)
        {
            this.context.RenderTarget.DrawLine(start, end, this.brushCache.GetOrCreate(color), width);
        }

        public void DrawRectangle(RectangleF rect, Color color, float width = 1.0f)
        {
            this.context.RenderTarget.DrawRectangle(rect, this.brushCache.GetOrCreate(color), width);
        }

        public void DrawText(Vector2 position, string text, Color color, float fontSize = 13f, string fontFamily = "Calibri")
        {
            var font = this.textFormatCache.GetOrCreate(fontFamily, fontSize);
            var brush = this.brushCache.GetOrCreate(color);

            using (var layout = new TextLayout(this.context.DirectWrite, text, font, float.MaxValue, float.MaxValue))
            {
                this.context.RenderTarget.DrawTextLayout(position, layout, brush);
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
                this.context.Dispose();
            }

            this.disposed = true;
        }
    }
}