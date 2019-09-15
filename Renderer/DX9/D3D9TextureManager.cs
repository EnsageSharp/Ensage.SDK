// <copyright file="D3D9TextureManager.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.DX9
{
    using System.ComponentModel.Composition;
    using System.Drawing;
    using System.IO;

    using SharpDX.Direct3D9;

    [Export]
    public sealed class D3D9TextureManager : D3TextureManager<D3D9Texture>
    {
        private readonly ID3D9Context renderContext;

        [ImportingConstructor]
        public D3D9TextureManager(ID3D9Context renderContext)
        {
            this.renderContext = renderContext;
        }

        protected override void LoadFromStream(string textureKey, Stream stream)
        {
            var bitmap = (Bitmap)Image.FromStream(stream);
            var bitmapData = (byte[])new ImageConverter().ConvertTo(bitmap, typeof(byte[]));
            var texture = Texture.FromMemory(
                this.renderContext.Device,
                bitmapData,
                bitmap.Width,
                bitmap.Height,
                0,
                Usage.None,
                Format.A1,
                Pool.Default,
                Filter.Default,
                Filter.Default,
                0);

            this.TextureCache[textureKey] = new D3D9Texture(texture, bitmap);
        }
    }
}