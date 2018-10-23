// <copyright file="TextFormatCache.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX11
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Reflection;

    

    using NLog;

    using SharpDX.DirectWrite;

    [Export(typeof(TextFormatCache))]
    public sealed class TextFormatCache : Dictionary<string, TextFormat>, IDisposable
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        [ImportingConstructor]
        public TextFormatCache([Import] ID3D11Context context)
        {
            this.Context = context;
        }

        private ID3D11Context Context { get; }

        public TextFormat Create(string familyName, float fontSize)
        {
            //Log.Debug($"Create Font {familyName}-{fontSize}");

            var key = $"{familyName}-{fontSize}";
            var format = new TextFormat(this.Context.DirectWrite, familyName, fontSize);

            this.Add(key, format);

            return format;
        }

        public TextFormat GetOrCreate(string familyName, float fontSize)
        {
            var key = $"{familyName}-{fontSize}";

            if (this.ContainsKey(key))
            {
                return this[key];
            }

            return this.Create(familyName, fontSize);
        }

        public void Dispose()
        {
            foreach (var value in this.Values)
            {
                value.Dispose();
            }
        }
    }
}