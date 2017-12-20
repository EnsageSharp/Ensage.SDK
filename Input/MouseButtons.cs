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

        XButton1 = 1 << 6,

        XButton1Up = (1 << 7) | XButton1,

        XButton1Down = (1 << 8) | XButton1,

        XButton2 = 1 << 9,

        XButton2Up = (1 << 10) | XButton2,

        XButton2Down = (1 << 11) | XButton2,
    }
}