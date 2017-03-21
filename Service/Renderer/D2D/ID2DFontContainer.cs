// <copyright file="ID2DFontContainer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer.D2D
{
    using System;
    using System.Collections.Generic;

    using SharpDX.DirectWrite;

    public interface ID2DFontContainer : IDictionary<string, TextFormat>, IDisposable
    {
        TextFormat Create(string name, string familyName, float size, bool bold = false, bool italic = false);
    }
}