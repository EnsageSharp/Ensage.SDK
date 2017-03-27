// <copyright file="KeyEventArgs.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Input
{
    using System;
    using System.Windows.Input;

    public class KeyEventArgs : EventArgs
    {
        public KeyEventArgs(Key key, bool process = true)
        {
            this.Key = key;
            this.Process = process;
        }

        public bool Handled { get; set; }

        public Key Key { get; set; }

        public bool Process { get; set; }
    }
}