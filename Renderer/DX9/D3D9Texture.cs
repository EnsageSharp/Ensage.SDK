// <copyright file="D3D9Texture.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer
{
    using System;
    using System.Drawing;

    using PlaySharp.Toolkit.Helper.Annotations;

    using SharpDX;
    using SharpDX.Direct3D9;

    public sealed class D3D9Texture : IDisposable
    {
        private bool disposed;

        public D3D9Texture([NotNull] Texture texture, Bitmap bitmap, [CanBeNull] string file = null)
        {
            this.Bitmap = bitmap;
            this.File = file;
            this.Texture = texture;
            this.Center = new Vector2((float)this.Bitmap.Width / 2.0f, (float)this.Bitmap.Height / 2.0f);
        }

        public Bitmap Bitmap { get; }

        public Vector2 Center { get; }

        [CanBeNull]
        public string File { get; internal set; }

        [NotNull]
        public Texture Texture { get; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.Texture.Dispose();
            }

            this.disposed = true;
        }
    }
}