// <copyright file="D3D11Renderer.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX11
{
    using System;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Renderer.Metadata;

    using NLog;

    using SharpDX;
    using SharpDX.Direct2D1;
    using SharpDX.DirectWrite;
    using SharpDX.Mathematics.Interop;

    using Color = System.Drawing.Color;

    [ExportRenderer(RenderMode.Dx11)]
    public sealed class D3D11Renderer : IRenderer
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly BrushCache brushCache;

        private readonly ID3D11Context context;

        private readonly TextFormatCache textFormatCache;

        private readonly D3D11TextureManager textureManager;

        private bool disposed;

        private RenderManager.EventHandler renderEventHandler;

        [ImportingConstructor]
        public D3D11Renderer(ID3D11Context context, BrushCache brushCache, TextFormatCache textFormatCache, D3D11TextureManager textureManager)
        {
            this.context = context;
            this.brushCache = brushCache;
            this.textFormatCache = textFormatCache;
            this.textureManager = textureManager;

            this.textureManager.VpkLoaded += this.OnVpkLoaded;
        }

        public event RenderManager.EventHandler Draw
        {
            add
            {
                this.renderEventHandler += value;
            }

            remove
            {
                this.renderEventHandler -= value;
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

        public void DrawFilledRectangle(RectangleF rect, Color color, Color borderColor, float borderWidth = 1.0f)
        {
            var target = this.context.RenderTarget;
            var backgroundBrush = this.brushCache.GetOrCreate(color);

            if (borderWidth > 0)
            {
                var borderBrush = this.brushCache.GetOrCreate(borderColor);

                for (var i = 1; i < (borderWidth + 1); i++)
                {
                    target.DrawRectangle(new RawRectangleF(rect.Left - i, rect.Top - i, rect.Right + i, rect.Bottom + i), borderBrush);
                }
            }

            target.FillRectangle(rect, backgroundBrush);
        }

        public void DrawFilledRectangle(RectangleF rect, Color color)
        {
            var target = this.context.RenderTarget;
            var backgroundBrush = this.brushCache.GetOrCreate(color);
            target.FillRectangle(rect, backgroundBrush);
        }

        public void DrawLine(Vector2 start, Vector2 end, Color color, float width = 1.0f)
        {
            this.context.RenderTarget.DrawLine(start, end, this.brushCache.GetOrCreate(color), width);
        }

        public void DrawRectangle(RectangleF rect, Color color, float borderWidth = 1.0f)
        {
            this.context.RenderTarget.DrawRectangle(rect, this.brushCache.GetOrCreate(color), borderWidth);
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
                layout.WordWrapping = WordWrapping.NoWrap;

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
                    position.Y += (position.Height / 2) - (font.FontSize * 0.6f);
                }

                this.context.RenderTarget.DrawTextLayout(position.Location, layout, brush);
            }
        }

        public void DrawTexture(string textureKey, Vector2 position, Vector2 size, float rotation = 0.0f, float opacity = 1.0f)
        {
            this.DrawTexture(textureKey, new RectangleF(position.X, position.Y, size.X, size.Y), rotation, opacity);
        }

        public void DrawTexture(string textureKey, RectangleF rect, float rotation = 0.0f, float opacity = 1.0f)
        {
            var textureEntry = this.textureManager.GetTexture(textureKey);

            if (rotation == 0.0f)
            {
                this.context.RenderTarget.DrawBitmap(textureEntry.Bitmap, rect, opacity, BitmapInterpolationMode.Linear);
            }
            else
            {
                var mtx = this.context.RenderTarget.Transform;

                var scaling = new Vector2(rect.Width / textureEntry.Bitmap.Size.Width, rect.Height / textureEntry.Bitmap.Size.Height);
                this.context.RenderTarget.Transform = Matrix3x2.Translation(-textureEntry.Center)
                                                      * Matrix3x2.Rotation(rotation)
                                                      * Matrix3x2.Translation(textureEntry.Center)
                                                      * Matrix3x2.Scaling(scaling)
                                                      * Matrix3x2.Translation(rect.Location);
                this.context.RenderTarget.DrawBitmap(textureEntry.Bitmap, opacity, BitmapInterpolationMode.Linear);

                this.context.RenderTarget.Transform = mtx;
            }
        }

        public Vector2 GetTextureSize(string textureKey)
        {
            return this.textureManager.GetTextureSize(textureKey);
        }

        public Vector2 MeasureText(string text, float fontSize = 13, string fontFamily = "Calibri")
        {
            var font = this.textFormatCache.GetOrCreate(fontFamily, fontSize);
            using (var layout = new TextLayout(this.context.DirectWrite, text, font, float.MaxValue, float.MaxValue))
            {
                var size = layout.Metrics;
                return new Vector2(size.Width, size.Height);
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
                this.context.Draw -= this.OnDraw;

                this.context.Dispose();
                this.textureManager.Dispose();
                this.textFormatCache.Dispose();
                this.brushCache.Dispose();
            }

            this.disposed = true;
        }

        private void OnDraw(object sender, EventArgs args)
        {
            if (this.renderEventHandler == null)
            {
                return;
            }

            // ReSharper disable once PossibleInvalidCastExceptionInForeachLoop
            foreach (RenderManager.EventHandler draw in this.renderEventHandler.GetInvocationList())
            {
                try
                {
                    draw(this);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        private void OnVpkLoaded(object sender, EventArgs e)
        {
            this.context.Draw += this.OnDraw;
            this.textureManager.VpkLoaded -= this.OnVpkLoaded;
        }
    }
}