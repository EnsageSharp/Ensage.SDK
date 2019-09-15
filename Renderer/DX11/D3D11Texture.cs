// <copyright file="D3D11Texture.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX11
{
    using Ensage.SDK.Renderer.Texture;

    using SharpDX;
    using SharpDX.Direct2D1;

    public sealed class D3D11Texture : D3Texture
    {
        private bool disposed;

        public D3D11Texture(Bitmap bitmap)
        {
            this.Bitmap = bitmap;
            this.Size = new Vector2(this.Bitmap.Size.Width, this.Bitmap.Size.Height);
            this.Center = new Vector2(this.Size.X / 2.0f, this.Size.Y / 2.0f);
        }

        public Bitmap Bitmap { get; }

        protected override void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.Bitmap.Dispose();
            }

            this.disposed = true;
        }
    }
}