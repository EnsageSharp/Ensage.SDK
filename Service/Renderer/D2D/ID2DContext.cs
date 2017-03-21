// <copyright file="ID2DContext.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer.D2D
{
    using SharpDX.Direct2D1;
    using SharpDX.Direct3D11;
    using SharpDX.DXGI;

    using Factory = SharpDX.Direct2D1.Factory;

    public interface ID2DContext
    {
        Texture2D BackBuffer { get; }

        Factory D2D1 { get; }

        SharpDX.DirectWrite.Factory DirectWrite { get; }

        Surface Surface { get; }

        SwapChain SwapChain { get; }

        RenderTarget Target { get; }
    }
}