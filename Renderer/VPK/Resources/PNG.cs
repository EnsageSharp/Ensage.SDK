// <copyright file="PNG.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.VPK.Resources
{
    using System.Drawing;
    using System.IO;

    internal class PNG : IBitmap
    {
        public PNG(Stream stream)
        {
            this.Bitmap = new Bitmap(stream);
        }

        public Bitmap Bitmap { get; }
    }
}