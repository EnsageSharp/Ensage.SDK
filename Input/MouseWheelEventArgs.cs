// <copyright file="MouseWheelEventArgs.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Input
{
    using System;

    public class MouseWheelEventArgs : EventArgs
    {
        public MouseWheelEventArgs(float delta)
        {
            this.Delta = delta;
        }

        public float Delta { get; }
    }
}