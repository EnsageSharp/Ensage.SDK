// <copyright file="D3D9Texture.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX9
{
    using System.Drawing;

    using Ensage.SDK.Renderer.Texture;

    using SharpDX;
    using SharpDX.Direct3D9;

    public sealed class D3D9Texture : D3Texture
    {
        private bool disposed;

        public D3D9Texture(Texture texture, Bitmap bitmap)
        {
            this.Bitmap = bitmap;
            this.Texture = texture;
            this.Size = new Vector2(this.Bitmap.Size.Width, this.Bitmap.Size.Height);
            this.Center = new Vector2(this.Size.X / 2.0f, this.Size.Y / 2.0f);
        }

        public Bitmap Bitmap { get; }

        public Texture Texture { get; }

        protected override void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.Texture.Dispose();
                this.Bitmap.Dispose();
            }

            this.disposed = true;
        }
    }
}