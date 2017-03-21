// <copyright file="KeyEventArgs.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Input
{
    using System;
    using System.Windows.Input;

    public class KeyEventArgs : EventArgs
    {
        public KeyEventArgs(uint key, bool process = true)
        {
            this.KeyCode = key;
            this.Process = process;
        }

        public bool Handled { get; set; }

        public Key Key => (Key)this.KeyCode;

        public uint KeyCode { get; internal set; }

        public bool Process { get; set; }
    }
}