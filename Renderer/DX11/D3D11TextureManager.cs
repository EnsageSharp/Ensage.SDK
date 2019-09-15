// <copyright file="D3D11TextureManager.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX11
{
    using System.ComponentModel.Composition;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    using Ensage.SDK.Renderer.Utils;

    using SharpDX.WIC;

    using Bitmap = SharpDX.Direct2D1.Bitmap;
    using PixelFormat = SharpDX.WIC.PixelFormat;

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
            // dx11 texture brightness fix
            using (var bitmap = Image.FromStream(stream).AdjustGamma(2f))
            {
                stream.Dispose();
                stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Png);
            }

            using (var bitmapDecoder = new BitmapDecoder(this.imagingFactory, stream, DecodeOptions.CacheOnDemand))
            {
                var frame = bitmapDecoder.GetFrame(0);
                using (var converter = new FormatConverter(this.imagingFactory))
                {
                    converter.Initialize(frame, PixelFormat.Format32bppPRGBA);
                    var bitmap = Bitmap.FromWicBitmap(this.renderContext.RenderTarget, converter);
                    this.TextureCache[textureKey] = new D3D11Texture(bitmap);
                }
            }
        }
    }
}