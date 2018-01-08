// <copyright file="RendererFontFlags.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer
{
    using System;

    /// <summary>
    /// Comaptible with DrawFontFlags from SharpDX9
    /// </summary>
    [Flags]
    public enum RendererFontFlags
    {
        Left = 0,

        Top = 0,

        Center = 1 << 0,

        Right = 1 << 1,

        VerticalCenter = 1 << 2,

        Bottom = 1 << 3,
    }
}