// <copyright file="D3D11Texture.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer
{
    using System;

    using PlaySharp.Toolkit.Helper.Annotations;

    using SharpDX.Direct2D1;

    public class D3D11Texture : IDisposable
    {
        private bool disposed;

        public D3D11Texture([NotNull] Bitmap bitmap, [CanBeNull] string file = null)
        {
            this.File = file;
            this.Bitmap = bitmap;
        }

        [NotNull]
        public Bitmap Bitmap { get; }

        [CanBeNull]
        public string File { get; internal set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
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