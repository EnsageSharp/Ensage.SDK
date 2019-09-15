// <copyright file="ResourceFile.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.VPK.Resources
{
    using System;
    using System.Drawing;
    using System.IO;

    internal class ResourceFile
    {
        private readonly IBitmap resource;

        public ResourceFile(Stream stream, string fileExtension)
        {
            switch (fileExtension.TrimStart('.'))
            {
                case "png":
                {
                    this.resource = new PNG(stream);
                    break;
                }
                case "vtex_c":
                {
                    this.resource = new VTex(new BinaryReader(stream));
                    break;
                }
                default:
                    throw new ArgumentException("File extension is not supported", fileExtension);
            }
        }

        public Bitmap GetBitmap()
        {
            return this.resource.Bitmap;
        }
    }
}