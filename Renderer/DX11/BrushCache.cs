// <copyright file="BrushCache.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX11
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

    [Export(typeof(BrushCache))]
    public class BrushCache : Dictionary<Color, SolidColorBrush>, IDisposable
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool disposed;

        [ImportingConstructor]
        public BrushCache([Import] ID3D11Context context)
        {
            this.Context = context;
            this.Create(Color.White);
            this.Create(Color.Black);
            this.Create(Color.Red);
            this.Create(Color.Blue);
            this.Create(Color.Green);
            this.Create(Color.Yellow);
        }

        private ID3D11Context Context { get; }

        public SolidColorBrush Create(Color color)
        {
            Log.Debug($"Create Brush {color} {color.R}-{color.G}-{color.B}-{color.A}");
            var brush = new SolidColorBrush(this.Context.RenderTarget, new RawColor4(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f));
            this.Add(color, brush);

            return brush;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public SolidColorBrush GetOrCreate(Color color)
        {
            if (this.ContainsKey(color))
            {
                return this[color];
            }

            return this.Create(color);
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