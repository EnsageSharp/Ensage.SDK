// <copyright file="VTexFormat.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.VPK.Content
{
    public enum VTexFormat : byte
    {
        IMAGE_FORMAT_NONE = 0,

        IMAGE_FORMAT_DXT1 = 1,

        IMAGE_FORMAT_DXT5 = 2,

        IMAGE_FORMAT_RGBA8888 = 4,

        IMAGE_FORMAT_RGBA16161616F = 10,

        IMAGE_FORMAT_PNG = 16,
    }
}