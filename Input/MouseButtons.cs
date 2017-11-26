// <copyright file="MouseButtons.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Input
{
    using System;

    [Flags]
    public enum MouseButtons
    {
        None = 0,

        Left = 1 << 0,

        LeftUp = (1 << 1) | Left,

        LeftDown = (1 << 2) | Left,

        Right = 1 << 3,

        RightUp = (1 << 4) | Right,

        RightDown = (1 << 5) | Right,

        XButton = 1 << 6,

        XButtonUp = (1 << 7) | XButton,

        XButtonDown = (1 << 8) | XButton,
    }
}