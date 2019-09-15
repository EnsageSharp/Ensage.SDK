// <copyright file="D3D11TextureManager.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX11
{
    using System.ComponentModel.Composition;
    using System.IO;

    using SharpDX.Direct2D1;
    using SharpDX.DXGI;
    using SharpDX.WIC;

    using AlphaMode = SharpDX.Direct2D1.AlphaMode;
    using Bitmap = SharpDX.Direct2D1.Bitmap;
    using PixelFormat = SharpDX.WIC.PixelFormat;
    using PixelFormat2D = SharpDX.Direct2D1.PixelFormat;

    [Export]
    public sealed class D3D11TextureManager : D3TextureManager<D3D11Texture>
    {
        private readonly ImagingFactory imagingFactory;

        private readonly ID3D11Context renderContext;

        private bool disposed;

        [ImportingConstructor]
        public D3D11TextureManager(ID3D11Context renderContext)
        {
            this.renderContext = renderContext;
            this.imagingFactory = new ImagingFactory();
        }

        protected override void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.imagingFactory.Dispose();
            }

            this.disposed = true;
        }

        protected override void LoadFromStream(string textureKey, Stream stream)
        {
            using (var bitmapDecoder = new BitmapDecoder(this.imagingFactory, stream, DecodeOptions.CacheOnDemand))
            {
                var frame = bitmapDecoder.GetFrame(0);
                using (var converter = new FormatConverter(this.imagingFactory))
                {
                    converter.Initialize(frame, PixelFormat.Format32bppPRGBA);
                    var bitmap = Bitmap.FromWicBitmap(this.renderContext.RenderTarget, converter, new BitmapProperties(new PixelFormat2D(Format.R8G8B8A8_UNorm_SRgb, AlphaMode.Premultiplied)));
                    this.TextureCache[textureKey] = new D3D11Texture(bitmap);
                }
            }
        }
    }
}