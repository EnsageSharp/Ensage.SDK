// <copyright file="MouseEventArgs.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Input
{
    using System;
    using System.Windows.Forms;

    using SharpDX;

    public class MouseEventArgs : EventArgs
    {
        public MouseEventArgs(MouseButtons buttons, Vector2 position, int clicks, bool process = true)
        {
            this.Position = position;
            this.Buttons = buttons;
            this.Clicks = clicks;
            this.Process = process;
        }

        public MouseButtons Buttons { get; internal set; }

        public int Clicks { get; internal set; }

        public bool Handled { get; set; }

        public Vector2 Position { get; internal set; }

        public bool Process { get; set; }
    }
}