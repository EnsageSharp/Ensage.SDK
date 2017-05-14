// <copyright file="IInputManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Input
{
    using System;
    using System.Windows.Forms;
    using System.Windows.Input;

    using PlaySharp.Toolkit.Helper;

    public interface IInputManager : IControllable
    {
        event EventHandler<KeyEventArgs> KeyDown;

        event EventHandler<KeyEventArgs> KeyUp;

        event EventHandler<MouseEventArgs> MouseClick;

        event EventHandler<MouseEventArgs> MouseMove;

        event EventHandler<MouseWheelEventArgs> MouseWheel;

        MouseButtons ActiveButtons { get; }

        bool IsKeyDown(Key key);

        Hotkey RegisterHotkey(string name, Key key, Action<KeyEventArgs> callback);

        void UnregisterHotkey(string name);
    }
}