// <copyright file="IRendererManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer
{
    public interface IRendererManager : IRenderer
    {
        ITextureManager TextureManager { get; }
    }
}