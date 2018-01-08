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

        Right = 1 << 1,

        XButton1 = 1 << 2,

        XButton2 = 1 << 3,

        HighestNoUpDownFlag = Left | Right | XButton1 | XButton2,

        LeftUp = (1 << 4) | Left,

        LeftDown = (1 << 5) | Left,

        RightUp = (1 << 6) | Right,

        RightDown = (1 << 7) | Right,

        XButton1Up = (1 << 8) | XButton1,

        XButton1Down = (1 << 9) | XButton1,

        XButton2Up = (1 << 10) | XButton2,

        XButton2Down = (1 << 11) | XButton2,
    }
}