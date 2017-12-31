// <copyright file="D3D9Renderer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX9
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ensage.Common.Extensions;
    using Ensage.SDK.Renderer.Metadata;

    using SharpDX;
    using SharpDX.Direct3D9;
    using SharpDX.Mathematics.Interop;

    using Color = System.Drawing.Color;

    [ExportRenderer(RenderMode.Dx9)]
    public sealed class D3D9Renderer : IRenderer
    {
        private readonly ID3D9Context context;

        private readonly FontCache fontCache;

        private readonly Line line;

        private readonly Sprite sprite;

        private readonly D3D9TextureManager textureManager;

        private bool disposed;

        [ImportingConstructor]
        public D3D9Renderer([Import] ID3D9Context context, [Import] FontCache fontCache, [Import] D3D9TextureManager textureManager)
        {
            this.context = context;
            this.fontCache = fontCache;
            this.textureManager = textureManager;
            this.line = new Line(this.context.Device)
                            {
                                //Antialias = true
                            };
            this.sprite = new Sprite(this.context.Device);

            context.PreReset += this.OnPreReset;
            context.PostReset += this.OnPostReset;
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
            var quality = 120f;
            var outRadius = radius / (float)Math.Cos((2 * Math.PI) / quality);
            var points = new List<Vector2>();
            var c = new ColorBGRA(color.R, color.G, color.B, color.A);

            for (var i = 1; i <= quality; i++)
            {
                var angle = (i * 2 * Math.PI) / quality;
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

        public void DrawRectangle(RectangleF rect, Color color, float width = 1.0f)
        {
            var c = new ColorBGRA(color.R, color.G, color.B, color.A);
            this.line.Width = width;
            this.line.Begin();

            try
            {
                float w = width / 2.0f;
                this.line.Draw(
                    new[]
                        {
                            // top left
                            new RawVector2(rect.X, rect.Y),

                            // to top right
                            new RawVector2(rect.X + rect.Width - w, rect.Y),

                            // to bottom right
                            new RawVector2(rect.X + rect.Width - w, rect.Y + rect.Height - w),

                            // to bottom left
                            new RawVector2(rect.X, rect.Y + rect.Height - w),

                            // to top left
                            new RawVector2(rect.X, rect.Y)
                        },
                    c);

                //// bottom | left to right
                // this.line.Draw(
                // new[] { new RawVector2(rect.X + (width / 2f), (rect.Y + rect.Height) - (width / 2f)), new RawVector2(rect.X + (width / 2f), rect.Y - (width / 2f)) },
                // c);

                //// right | top to bottom
                // this.line.Draw(new[] { new RawVector2(rect.X + rect.Width, rect.Y + rect.Height), new RawVector2(rect.X, rect.Y + rect.Height) }, c);

                //// bottom | left to right
                // this.line.Draw(new[] { new RawVector2(rect.X, rect.Y), new RawVector2(rect.X + rect.Width, rect.Y) }, c);

                //// left | top to bottom
                // this.line.Draw(
                // new[]
                // {
                // new RawVector2((rect.X + rect.Width) - (width / 2f), rect.Y - (width / 2f)),
                // new RawVector2((rect.X + rect.Width) - (width / 2f), (rect.Y + rect.Height) - (width / 2f))
                // },
                // c);
            }
            finally
            {
                this.line.End();
            }
        }

        public void DrawText(Vector2 position, string text, Color color, float fontSize = 13f, string fontFamily = "Calibri")
        {
            var font = this.fontCache.GetOrCreate(fontFamily, fontSize);
            font.DrawText(null, text, (int)position.X, (int)position.Y, new ColorBGRA(color.R, color.G, color.B, color.A));
        }

        public void DrawTexture(string textureKey, RectangleF rect, float rotation = 0, float opacity = 1)
        {
            var textureEntry = this.textureManager.GetTexture(textureKey);
            if (textureEntry == null)
            {
                throw new TextureNotFoundException(textureKey);
            }

            this.sprite.Begin(SpriteFlags.AlphaBlend);

            var matrix = this.sprite.Transform;
            var scaling = new Vector2((float)rect.Width / textureEntry.Bitmap.Width, (float)rect.Height / textureEntry.Bitmap.Height);
            if (rotation == 0.0f)
            {
                this.sprite.Transform = Matrix.Scaling(scaling.X, scaling.Y, 0) * Matrix.Translation(rect.X, rect.Y, 0);
                this.sprite.Draw(textureEntry.Texture, SharpDX.Color.White);
            }
            else
            {
                var center = textureEntry.Center.ToVector3();
                this.sprite.Transform = Matrix.Translation(-center)
                                        * Matrix.RotationZ(rotation)
                                        * Matrix.Translation(center)
                                        * Matrix.Scaling(scaling.X, scaling.Y, 0)
                                        * Matrix.Translation(rect.X, rect.Y, 0);
                this.sprite.Draw(textureEntry.Texture, SharpDX.Color.White);
            }

            this.sprite.Transform = matrix;
            this.sprite.End();
        }

        public Vector2 GetTextureSize(string textureKey)
        {
            return this.textureManager.GetTextureSize(textureKey);
        }

        public Vector2 MessureText(string text, float fontSize = 13, string fontFamily = "Calibri")
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
                this.textureManager.Dispose();
                this.context.Dispose();
            }

            this.disposed = true;
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
    }
}