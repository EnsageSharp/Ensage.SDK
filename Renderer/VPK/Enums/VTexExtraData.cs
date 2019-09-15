// <copyright file="VTexExtraData.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.VPK.Enums
{
    // ReSharper disable InconsistentNaming
    internal enum VTexExtraData : byte
    {
        UNKNOWN = 0,

        FALLBACK_BITS = 1,

        SHEET = 2,

        FILL_TO_POWER_OF_TWO = 3,

        COMPRESSED_MIP_SIZE = 4,
    }
}