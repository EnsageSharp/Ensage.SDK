﻿// <copyright file="FontCache.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX9
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Reflection;

    

    using NLog;

    using SharpDX.Direct3D9;

    [Export(typeof(FontCache))]
    public sealed class FontCache : Dictionary<string, Font>, IDisposable
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        [ImportingConstructor]
        public FontCache([Import] ID3D9Context context)
        {
            this.Context = context;
            this.Context.PreReset += this.PreReset;
            this.Context.PostReset += this.PostReset;
        }

        private ID3D9Context Context { get; }

        public Font Create(string familyName, float fontSize)
        {
            //Log.Debug($"Create Font {familyName}-{fontSize}");

            var key = $"{familyName}-{fontSize}";
            var font = new Font(
                this.Context.Device,
                new FontDescription
                {
                    FaceName = familyName,
                    Height = (int)fontSize + 6,
                    OutputPrecision = FontPrecision.Default,
                    Quality = FontQuality.Default,
                    Weight = FontWeight.Thin,
                });

            this.Add(key, font);

            return font;
        }

        public Font GetOrCreate(string familyName, float fontSize)
        {
            var key = $"{familyName}-{fontSize}";

            if (this.ContainsKey(key))
            {
                return this[key];
            }

            return this.Create(familyName, fontSize);
        }

        private void PostReset(object sender, EventArgs e)
        {
            foreach (var value in this.Values)
            {
                value.OnResetDevice();
            }
        }

        private void PreReset(object sender, EventArgs e)
        {
            foreach (var value in this.Values)
            {
                value.OnLostDevice();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed;

        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (var value in this.Values)
                {
                    value.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}