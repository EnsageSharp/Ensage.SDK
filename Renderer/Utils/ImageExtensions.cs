// <copyright file="ImageExtensions.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.Utils
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    using SharpDX;

    using Point = System.Drawing.Point;
    using Rectangle = System.Drawing.Rectangle;
    using RectangleF = System.Drawing.RectangleF;

    internal static class ImageExtensions
    {
        public static Bitmap AdjustBrightness(this Image image, float value)
        {
            using (image)
            {
                var attributes = new ImageAttributes();

                var b = value / 255f;
                var cm = new ColorMatrix(
                    new[] { new[] { 1f, 0f, 0f, 0f, 0f }, new[] { 0f, 1f, 0f, 0f, 0f }, new[] { 0f, 0f, 1f, 0f, 0f }, new[] { 0f, 0f, 0f, 1f, 0f }, new[] { b, b, b, 0f, 1f } });
                attributes.SetColorMatrix(cm);

                var bitmap = new Bitmap(image.Width, image.Height);

                using (var gr = Graphics.FromImage(bitmap))
                {
                    var points = new[] { new Point(0, 0), new Point(image.Width, 0), new Point(0, image.Height) };
                    var rect = new Rectangle(0, 0, image.Width, image.Height);

                    gr.DrawImage(image, points, rect, GraphicsUnit.Pixel, attributes);
                }

                return bitmap;
            }
        }

        public static Bitmap AdjustColor(this Image source, Vector4 colorRatio)
        {
            var cm = new ColorMatrix(
                new[]
                {
                    new[] { 1f * colorRatio.X, 0f, 0f, 0f, 0f },
                    new[] { 0f, 1f * colorRatio.Y, 0f, 0f, 0f },
                    new[] { 0f, 0f, 1f * colorRatio.Z, 0f, 0f },
                    new[] { 0f, 0f, 0f, 1f * colorRatio.W, 0f },
                    new[] { 0f, 0f, 0f, 0f, 1f }
                });
            var attributes = new ImageAttributes();
            attributes.SetColorMatrix(cm);

            Point[] points = { new Point(0, 0), new Point(source.Width, 0), new Point(0, source.Height), };
            var rect = new Rectangle(0, 0, source.Width, source.Height);
            var bitmap = new Bitmap(source.Width, source.Height);

            using (var gr = Graphics.FromImage(bitmap))
            {
                gr.DrawImage(source, points, rect, GraphicsUnit.Pixel, attributes);
            }

            return bitmap;
        }

        public static Bitmap AdjustGamma(this Image source, float value)
        {
            var attributes = new ImageAttributes();
            attributes.SetGamma(value, ColorAdjustType.Bitmap);

            var bitmap = new Bitmap(source.Width, source.Height);

            using (var gr = Graphics.FromImage(bitmap))
            {
                var points = new[] { new Point(0, 0), new Point(source.Width, 0), new Point(0, source.Height) };
                var rect = new Rectangle(0, 0, source.Width, source.Height);

                gr.DrawImage(source, points, rect, GraphicsUnit.Pixel, attributes);
            }

            return bitmap;
        }

        public static Bitmap Pie(this Image source, int pct)
        {
            var bitmap = new Bitmap(source.Width, source.Height);

            using (var gp = new GraphicsPath())
            {
                gp.AddPie(0, 0, source.Width, source.Height, -90, 3.6f * pct);

                using (var gr = Graphics.FromImage(bitmap))
                {
                    gr.SetClip(gp);
                    gr.DrawImage(source, new PointF(0, 0));
                }
            }

            return bitmap;
        }

        public static Bitmap Round(this Image source, float xRatio)
        {
            var size = Math.Min(source.Width, source.Height);
            var bitmap = new Bitmap(size, size);

            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0, size, size);

                using (var gr = Graphics.FromImage(bitmap))
                {
                    gr.SetClip(gp);
                    gr.DrawImage(source, new RectangleF((source.Width - size) * -xRatio, (source.Height - size) * -0.5f, source.Width, source.Height));
                }
            }

            return bitmap;
        }

        public static Bitmap Square(this Image source)
        {
            var size = Math.Min(source.Width, source.Height);
            var bitmap = new Bitmap(size, size);

            using (var gp = new GraphicsPath())
            {
                gp.AddRectangle(new Rectangle(0, 0, size, size));

                using (var gr = Graphics.FromImage(bitmap))
                {
                    gr.SetClip(gp);
                    gr.DrawImage(source, new RectangleF((source.Width - size) * -0.5f, (source.Height - size) * -0.5f, source.Width, source.Height));
                }
            }

            return bitmap;
        }
    }
}