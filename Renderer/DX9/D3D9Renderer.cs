// <copyright file="D3D9Renderer.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX9
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Renderer.Metadata;

    using NLog;

    using SharpDX;
    using SharpDX.Direct3D9;
    using SharpDX.Mathematics.Interop;

    using Color = System.Drawing.Color;

    [ExportRenderer(RenderMode.Dx9)]
    public sealed class D3D9Renderer : IRenderer
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly ID3D9Context context;

        private readonly FontCache fontCache;

        private readonly Line line;

        private readonly Sprite sprite;

        private readonly D3D9TextureManager textureManager;

        private bool disposed;

        private RenderManager.EventHandler renderEventHandler;

        [ImportingConstructor]
        public D3D9Renderer(ID3D9Context context, FontCache fontCache, D3D9TextureManager textureManager)
        {
            this.context = context;
            this.fontCache = fontCache;
            this.textureManager = textureManager;
            this.line = new Line(this.context.Device);
            this.sprite = new Sprite(this.context.Device);

            this.context.PreReset += this.OnPreReset;
            this.context.PostReset += this.OnPostReset;
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
                // ReSharper disable once DelegateSubtraction
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
            const float Quality = 120f;
            var outRadius = radius / (float)Math.Cos((2 * Math.PI) / Quality);
            var points = new List<Vector2>();
            var c = new ColorBGRA(color.R, color.G, color.B, color.A);

            for (var i = 1; i <= Quality; i++)
            {
                var angle = (i * 2 * Math.PI) / Quality;
                var point = new Vector2(center.X + (outRadius * (float)Math.Cos(angle)), center.Y + (outRadius * (float)Math.Sin(angle)));

                points.Add(point);
            }

            this.line.Width = width;
            this.line.Begin();

            try
            {
                for (var i = 0; i <= (points.Count - 1); i++)
                {
                    var nextIndex = points.Count - 1 == i ? 0 : i + 1;
                    this.line.Draw(new RawVector2[] { points[i], points[nextIndex] }, c);
                }
            }
            finally
            {
                this.line.End();
            }
        }

        public void DrawFilledRectangle(RectangleF rect, Color color, Color borderColor, float borderWidth = 1.0f)
        {
            this.DrawFilledRectangle(new RectangleF(rect.X - borderWidth, rect.Y - borderWidth, rect.Width + (borderWidth * 2), rect.Height + (borderWidth * 2)), borderColor);
            this.DrawFilledRectangle(rect, color);
        }

        public void DrawFilledRectangle(RectangleF rect, Color color)
        {
            var c = new ColorBGRA(color.R, color.G, color.B, color.A);
            var heightFix = new Vector2(0, rect.Height / 2);
            this.line.Width = rect.Height;
            this.line.Begin();

            try
            {
                this.line.Draw(new RawVector2[] { rect.TopLeft + heightFix, rect.TopRight + heightFix }, c);
            }
            finally
            {
                this.line.End();
            }
        }

        public void DrawLine(Vector2 start, Vector2 end, Color color, float width = 1.0f)
        {
            this.line.Width = width;
            this.line.Begin();

            try
            {
                this.line.Draw(new RawVector2[] { start, end }, new ColorBGRA(color.R, color.G, color.B, color.A));
            }
            finally
            {
                this.line.End();
            }
        }

        public void DrawRectangle(RectangleF rect, Color color, float borderWidth = 1.0f)
        {
            var c = new ColorBGRA(color.R, color.G, color.B, color.A);
            this.line.Width = borderWidth;
            this.line.Begin();

            try
            {
                this.line.Draw(new RawVector2[] { rect.TopLeft, rect.TopRight }, c);
                this.line.Draw(new RawVector2[] { rect.TopRight, rect.BottomRight }, c);
                this.line.Draw(new RawVector2[] { rect.BottomRight, rect.BottomLeft }, c);
                this.line.Draw(new RawVector2[] { rect.BottomLeft, rect.TopLeft }, c);
            }
            finally
            {
                this.line.End();
            }
        }

        public void DrawText(Vector2 position, string text, Color color, float fontSize = 13f, string fontFamily = "Calibri")
        {
            if (text.Length == 0)
            {
                return;
            }

            var font = this.fontCache.GetOrCreate(fontFamily, fontSize);
            font.DrawText(null, text, (int)position.X, (int)position.Y, new ColorBGRA(color.R, color.G, color.B, color.A));
        }

        public void DrawText(RectangleF position, string text, Color color, RendererFontFlags flags = RendererFontFlags.Left, float fontSize = 13f, string fontFamily = "Calibri")
        {
            if (text.Length == 0)
            {
                return;
            }

            var font = this.fontCache.GetOrCreate(fontFamily, fontSize);
            font.DrawText(null, text, position, (FontDrawFlags)flags | FontDrawFlags.NoClip, new ColorBGRA(color.R, color.G, color.B, color.A));
        }

        public void DrawTexture(string textureKey, Vector2 position, Vector2 size, float rotation = 0.0f, float opacity = 1.0f)
        {
            this.DrawTexture(textureKey, new RectangleF(position.X, position.Y, size.X, size.Y), rotation, opacity);
        }

        public void DrawTexture(string textureKey, RectangleF rect, float rotation = 0, float opacity = 1)
        {
            var textureEntry = this.textureManager.GetTexture(textureKey);

            this.sprite.Begin(SpriteFlags.AlphaBlend);

            byte alpha = 255;
            if (opacity < 1)
            {
                alpha = (byte)(alpha * Math.Max(opacity, 0));
            }

            var color = new RawColorBGRA(255, 255, 255, alpha);
            var matrix = this.sprite.Transform;
            var scaling = new Vector2(rect.Width / textureEntry.Bitmap.Width, rect.Height / textureEntry.Bitmap.Height);
            if (rotation == 0.0f)
            {
                this.sprite.Transform = Matrix.Scaling(scaling.X, scaling.Y, 0) * Matrix.Translation(rect.X, rect.Y, 0);
                this.sprite.Draw(textureEntry.Texture, color);
            }
            else
            {
                var center = textureEntry.Center.ToVector3();
                this.sprite.Transform = Matrix.Translation(-center)
                                        * Matrix.RotationZ(rotation)
                                        * Matrix.Translation(center)
                                        * Matrix.Scaling(scaling.X, scaling.Y, 0)
                                        * Matrix.Translation(rect.X, rect.Y, 0);
                this.sprite.Draw(textureEntry.Texture, color);
            }

            this.sprite.Transform = matrix;
            this.sprite.End();
        }

        public Vector2 GetTextureSize(string textureKey)
        {
            return this.textureManager.GetTextureSize(textureKey);
        }

        public Vector2 MeasureText(string text, float fontSize = 13, string fontFamily = "Calibri")
        {
            var font = this.fontCache.GetOrCreate(fontFamily, fontSize);
            var rect = font.MeasureText(null, text, FontDrawFlags.Left);
            return new Vector2(rect.Right - rect.Left, rect.Bottom - rect.Top);
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
                this.fontCache.Dispose();
                this.line.Dispose();
                this.sprite.Dispose();
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

        private void OnPostReset(object sender, EventArgs eventArgs)
        {
            this.line.OnResetDevice();
            this.sprite.OnResetDevice();
        }

        private void OnPreReset(object sender, EventArgs eventArgs)
        {
            this.line.OnLostDevice();
            this.sprite.OnLostDevice();
        }

        private void OnVpkLoaded(object sender, EventArgs e)
        {
            this.context.Draw += this.OnDraw;
            this.textureManager.VpkLoaded -= this.OnVpkLoaded;
        }
    }
}