// <copyright file="D2DBrushContainer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer.D2D
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Drawing;

    using SharpDX.Direct2D1;
    using SharpDX.Mathematics.Interop;

    [Export(typeof(ID2DBrushContainer))]
    public class D2DBrushContainer : Dictionary<string, SolidColorBrush>, ID2DBrushContainer
    {
        private bool disposed;

        [Import(typeof(ID2DContext))]
        protected ID2DContext Context { get; private set; }

        public SolidColorBrush this[Color color]
        {
            get
            {
                return this[color.ToString()];
            }
        }

        public SolidColorBrush Create(string name, Color color)
        {
            if (color.A == 0)
            {
                color = Color.FromArgb(255, color);
            }

            var brush = new SolidColorBrush(
                this.Context.Target,
                new RawColor4(color.R, color.G, color.B, color.A / 255.0f));

            this.Add(name, brush);

            return brush;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            foreach (var brush in this.Values)
            {
                if (!brush.IsDisposed)
                {
                    brush.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}