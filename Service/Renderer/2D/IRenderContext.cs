// <copyright file="IRenderContext.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer
{
    using SharpDX.Direct2D1;

    public interface IRenderContext
    {
        Factory Factory { get; }

        RenderTarget Target { get; }
    }
}