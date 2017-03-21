// <copyright file="ID2D1Context.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer.D2D
{
    using SharpDX.Direct2D1;

    public interface ID2D1Context
    {
        Factory D2D1 { get; }

        SharpDX.DirectWrite.Factory DirectWrite { get; }

        RenderTarget Target { get; }
    }
}