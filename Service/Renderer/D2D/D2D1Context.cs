// <copyright file="D2D1Context.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer.D2D
{
    using SharpDX.Direct2D1;

    public class D2D1Context : ID2D1Context
    {
        public Factory D2D1 { get; internal set; } = new Factory();

        public SharpDX.DirectWrite.Factory DirectWrite { get; internal set; } = new SharpDX.DirectWrite.Factory();

        public RenderTarget Target { get; internal set; }
    }
}