// <copyright file="MouseWheelEventArgs.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Input
{
    using System;

    public class MouseWheelEventArgs : EventArgs
    {
        public MouseWheelEventArgs(float delta, bool process = true)
        {
            this.Delta = delta;
            this.Process = process;
        }

        public float Delta { get; }

        public bool Process { get; set; }
    }
}