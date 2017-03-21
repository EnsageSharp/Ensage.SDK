// <copyright file="D2DContext.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Renderer.D2D
{
    using System;

    using SharpDX.Direct2D1;
    using SharpDX.Direct3D11;
    using SharpDX.DXGI;

    using AlphaMode = SharpDX.Direct2D1.AlphaMode;
    using Factory = SharpDX.Direct2D1.Factory;

    public class D2DContext : ID2DContext, IDisposable
    {
        private bool disposed;

        public D2DContext()
        {
            this.Target = new RenderTarget(
                this.D2D1,
                this.Surface,
                new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));
        }

        public Texture2D BackBuffer => this.SwapChain.GetBackBuffer<Texture2D>(0);

        public Factory D2D1 { get; } = new Factory();

        public SharpDX.DirectWrite.Factory DirectWrite { get; } = new SharpDX.DirectWrite.Factory();

        public Surface Surface => this.BackBuffer.QueryInterface<Surface>();

        public SwapChain SwapChain => Drawing.SwapChain;

        public RenderTarget Target { get; }

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

            this.Target?.Dispose();
            this.D2D1?.Dispose();
            this.DirectWrite?.Dispose();

            this.disposed = true;
        }
    }
}