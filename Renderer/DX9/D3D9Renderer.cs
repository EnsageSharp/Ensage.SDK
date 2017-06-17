// <copyright file="D3D9Renderer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX9
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Renderer.Metadata;

    using SharpDX;
    using SharpDX.Direct3D9;
    using SharpDX.Mathematics.Interop;

    using Color = System.Drawing.Color;

    [ExportRenderer(RenderMode.Dx9)]
    public class D3D9Renderer : IRenderer
    {
        private readonly ID3D9Context context;

        private Font font;

        private Line line;

        private Sprite sprite;

        [ImportingConstructor]
        public D3D9Renderer([Import] ID3D9Context context)
        {
            this.context = context;

            this.line = new Line(this.context.Device);
            this.sprite = new Sprite(this.context.Device);
            this.font = new Font(
                this.context.Device,
                new FontDescription
                {
                    FaceName = "Calibri",
                    Height = 13,
                    OutputPrecision = FontPrecision.Default,
                    Quality = FontQuality.ClearTypeNatural
                });

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

        public void DrawCircle(Vector2 center, float radius, Color color)
        {
            var quality = 60f;
            var outRadius = radius / (float)Math.Cos((2 * Math.PI) / quality);
            var points = new List<Vector2>();
            var c = new ColorBGRA(color.R, color.G, color.B, color.A);

            for (var i = 1; i <= quality; i++)
            {
                var angle = (i * 2 * Math.PI) / quality;
                var point = new Vector2(center.X + (outRadius * (float)Math.Cos(angle)), center.Y + (outRadius * (float)Math.Sin(angle)));

                points.Add(point);
            }

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
                this.line.Draw(new[] { new RawVector2(rect.X, rect.Y), new RawVector2(rect.X + rect.Width, rect.Y) }, c);
                this.line.Draw(new[] { new RawVector2(rect.X + rect.Width, rect.Y), new RawVector2(rect.X + rect.Width, rect.Y + rect.Height) }, c);
                this.line.Draw(new[] { new RawVector2(rect.X + rect.Width, rect.Y + rect.Height), new RawVector2(rect.X, rect.Y + rect.Height) }, c);
                this.line.Draw(new[] { new RawVector2(rect.X, rect.Y + rect.Height), new RawVector2(rect.X, rect.Y) }, c);
            }
            finally
            {
                this.line.End();
            }
        }

        public void DrawText(Vector2 position, string text, Color color, string fontFamily = "Calibri", float fontSize = 13)
        {
            this.font.DrawText(null, text, (int)position.X, (int)position.Y, new ColorBGRA(color.R, color.G, color.B, color.A));
        }

        private void OnPostReset(object sender, EventArgs eventArgs)
        {
            this.line.OnResetDevice();
            this.sprite.OnResetDevice();
            this.font.OnResetDevice();
        }

        private void OnPreReset(object sender, EventArgs eventArgs)
        {
            this.line.OnLostDevice();
            this.sprite.OnLostDevice();
            this.font.OnLostDevice();
        }
    }
}