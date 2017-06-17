// <copyright file="ID3D9Context.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX9
{
    using System;

    using SharpDX.Direct3D9;

    public interface ID3D9Context
    {
        event EventHandler Draw;

        event EventHandler PostReset;

        event EventHandler PreReset;

        Device Device { get; }
    }
}