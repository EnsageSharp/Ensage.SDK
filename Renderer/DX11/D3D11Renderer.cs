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
    using SharpDX.Direct3D9;
    using SharpDX.DirectWrite;
    using SharpDX.Mathematics.Interop;

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

        public ITextureManager TextureManager
        {
            get
            {
                return this.textureManager;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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

        public void DrawText(RectangleF position, string text, Color color, RendererFontFlags flags = RendererFontFlags.Left, float fontSize = 13f, string fontFamily = "Calibri")
        {
            var font = this.textFormatCache.GetOrCreate(fontFamily, fontSize);
            var brush = this.brushCache.GetOrCreate(color);

            using (var layout = new TextLayout(this.context.DirectWrite, text, font, position.Width, position.Height))
            {
                if ((flags & RendererFontFlags.Center) == RendererFontFlags.Center)
                {
                    layout.TextAlignment = TextAlignment.Center;
                }
                else if ((flags & RendererFontFlags.Right) == RendererFontFlags.Right)
                {
                    layout.TextAlignment = TextAlignment.Trailing;
                }

                if ((flags & RendererFontFlags.VerticalCenter) == RendererFontFlags.VerticalCenter)
                {
                    position.Y = (position.Y + (position.Height / 2)) - (font.FontSize / 2);
                }

                this.context.RenderTarget.DrawTextLayout(new RawVector2(position.X, position.Y), layout, brush);
            }
        }

        public void DrawTexture(string textureKey, RectangleF rect, float rotation = 0.0f, float opacity = 1.0f)
        {
            var textureEntry = this.textureManager.GetTexture(textureKey);
            if (textureEntry == null)
            {
                throw new TextureNotFoundException(textureKey);
            }

            if (rotation == 0.0f)
            {
                this.context.RenderTarget.DrawBitmap(textureEntry.Bitmap, rect, opacity, BitmapInterpolationMode.Linear);
            }
            else
            {
                var mtx = this.context.RenderTarget.Transform;

                var scaling = new Vector2((float)rect.Width / textureEntry.Bitmap.Size.Width, (float)rect.Height / textureEntry.Bitmap.Size.Height);
                this.context.RenderTarget.Transform = Matrix3x2.Translation(-textureEntry.Center)
                                                      * Matrix3x2.Rotation(rotation)
                                                      * Matrix3x2.Translation(textureEntry.Center)
                                                      * Matrix3x2.Scaling(scaling)
                                                      * Matrix3x2.Translation(rect.Location);
                this.context.RenderTarget.DrawBitmap(textureEntry.Bitmap, opacity, BitmapInterpolationMode.Linear);

                this.context.RenderTarget.Transform = mtx;
            }
        }

        public Vector2 MessureText(string text, float fontSize = 13, string fontFamily = "Calibri")
        {
            var font = this.textFormatCache.GetOrCreate(fontFamily, fontSize);
            using (var layout = new TextLayout(this.context.DirectWrite, text, font, float.MaxValue, float.MaxValue))
            {
                var size = layout.Metrics;
                return new Vector2(size.Width, size.Height);
            }
        }

        public Vector2 GetTextureSize(string textureKey)
        {
            return this.textureManager.GetTextureSize(textureKey);
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.textureManager.Dispose();
                this.context.Dispose();
            }

            this.disposed = true;
        }
    }
}