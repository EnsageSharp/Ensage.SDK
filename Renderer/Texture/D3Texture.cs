// <copyright file="D3Texture.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.Texture
{
    using System;

    using SharpDX;

    public abstract class D3Texture : IDisposable
    {
        public Vector2 Center { get; protected set; }

        public Vector2 Size { get; protected set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);
    }
}