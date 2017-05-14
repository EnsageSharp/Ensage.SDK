// <copyright file="D2DBrushContainer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.D2D
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Drawing;
    using System.Reflection;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using SharpDX.Direct2D1;
    using SharpDX.Mathematics.Interop;

    [Export(typeof(ID2DBrushContainer))]
    public class D2DBrushContainer : Dictionary<string, SolidColorBrush>, ID2DBrushContainer
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool disposed;

        public D2DBrushContainer()
        {
            this.Create(Color.White);
            this.Create(Color.Black);
            this.Create(Color.Red);
            this.Create(Color.Blue);
            this.Create(Color.Green);
            this.Create(Color.Yellow);
        }

        [Import(typeof(ID2DContext))]
        protected ID2DContext Context { get; private set; }

        public SolidColorBrush this[Color color]
        {
            get
            {
                return this[color.ToString()];
            }
        }

        public SolidColorBrush Create(Color color)
        {
            return this.Create(color.ToString(), color);
        }

        public SolidColorBrush Create(string name, Color color)
        {
            if (color.A == 0)
            {
                color = Color.FromArgb(255, color);
            }

            var brush = new SolidColorBrush(this.Context.Target, new RawColor4(color.R, color.G, color.B, color.A / 255.0f));

            Log.Debug($"Create Brush {name} {color.R}-{color.G}-{color.B}-{color.A}");
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