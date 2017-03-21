// <copyright file="D2DFontContainer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer.D2D
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Reflection;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using SharpDX.DirectWrite;

    [Export(typeof(ID2DFontContainer))]
    public class D2DFontContainer : Dictionary<string, TextFormat>, ID2DFontContainer
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool disposed;

        [Import(typeof(ID2DContext))]
        protected ID2DContext Context { get; private set; }

        public TextFormat Create(string name, string familyName, float size, bool bold = false, bool italic = false)
        {
            var format = new TextFormat(
                this.Context.DirectWrite,
                familyName,
                bold ? FontWeight.Bold : FontWeight.Normal,
                italic ? FontStyle.Italic : FontStyle.Normal,
                size);

            Log.Debug($"Create Font {name} {familyName}-{size}");
            this.Add(name, format);

            return format;
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

            foreach (var format in this.Values)
            {
                if (!format.IsDisposed)
                {
                    format.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}