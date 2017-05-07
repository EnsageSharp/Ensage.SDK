// <copyright file="InputManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Input
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Windows.Input;

    using Ensage.SDK.EventHandler;
    using Ensage.SDK.Input.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    [ExportInputManager]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "StyleCop.SA1310")]
    public class InputManager : IInputManager, IDisposable
    {
        private const uint WM_KEYDOWN = 0x0100;

        private const uint WM_KEYUP = 0x0101;

        private const uint WM_LBUTTONDBLCLK = 0x0203;

        private const uint WM_LBUTTONDOWN = 0x0201;

        private const uint WM_LBUTTONUP = 0x0202;

        private const uint WM_MOUSEMOVE = 0x0200;

        private const uint WM_MOUSEWHEEL = 0x020A;

        private const uint WM_RBUTTONDBLCLK = 0x0206;

        private const uint WM_RBUTTONDOWN = 0x0204;

        private const uint WM_RBUTTONUP = 0x0205;

        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly List<Hotkey> hotkeys = new List<Hotkey>();

        private bool disposed;

        public InputManager()
        {
            Game.OnWndProc += this.OnWndProc;
            this.KeyDown += this.HotkeyHandler;
        }

        public event EventHandler<KeyEventArgs> KeyDown;

        public event EventHandler<KeyEventArgs> KeyUp;

        public event EventHandler<MouseEventArgs> MouseClick;

        public event EventHandler<MouseEventArgs> MouseMove;

        public event EventHandler<MouseWheelEventArgs> MouseWheel;

        public MouseButtons ActiveButtons { get; private set; }

        public IEnumerable<Hotkey> Hotkeys => this.hotkeys.AsReadOnly();

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool IsKeyDown(Key key)
        {
            return Game.IsKeyDown(key);
        }

        public Hotkey RegisterHotkey(string name, Key key, Action<KeyEventArgs> callback)
        {
            if (this.hotkeys.Any(e => e.Name == name))
            {
                throw new ArgumentException($"Hotkey ({name}) with the same {nameof(name)} already asigned");
            }

            var hotkey = new Hotkey(name, key, callback);

            this.hotkeys.Add(hotkey);

            return hotkey;
        }

        public void UnregisterHotkey(string name)
        {
            this.hotkeys.RemoveAll(h => h.Name == name);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                Game.OnWndProc -= this.OnWndProc;
            }

            this.disposed = true;
        }

        private void FireKeyDown(WndEventArgs args)
        {
            if (Game.IsChatOpen)
            {
                return;
            }

            var key = KeyInterop.KeyFromVirtualKey((int)args.WParam);
            var data = new KeyEventArgs(key, args.Process);
            this.KeyDown?.Invoke(this, data);

            // update Process state
            args.Process = args.Process && data.Process;
        }

        private void FireKeyUp(WndEventArgs args)
        {
            if (Game.IsChatOpen)
            {
                return;
            }

            var key = KeyInterop.KeyFromVirtualKey((int)args.WParam);
            var data = new KeyEventArgs(key, args.Process);
            this.KeyUp?.Invoke(this, data);

            // update Process state
            args.Process = args.Process && data.Process;
        }

        private void FireMouseClick(WndEventArgs args)
        {
            var data = new MouseEventArgs(MouseButtons.None, Game.MouseScreenPosition, 0, args.Process);

            switch (args.Msg)
            {
                case WM_LBUTTONUP:
                    data.Buttons = MouseButtons.Left;
                    data.Clicks = 1;
                    break;

                case WM_RBUTTONUP:
                    data.Buttons = MouseButtons.Right;
                    data.Clicks = 1;
                    break;

                case WM_LBUTTONDBLCLK:
                    data.Buttons = MouseButtons.Left;
                    data.Clicks = 2;
                    break;

                case WM_RBUTTONDBLCLK:
                    data.Buttons = MouseButtons.Right;
                    data.Clicks = 2;
                    break;
            }

            if (data.Clicks > 0)
            {
                this.MouseClick?.Invoke(this, data);
                Messenger<MouseEventArgs>.Publish(data);

                // update Process state
                args.Process = args.Process && data.Process;
            }
        }

        private void FireMouseMove(WndEventArgs args)
        {
            var data = new MouseEventArgs(this.ActiveButtons, Game.MouseScreenPosition, 0, args.Process);

            this.MouseMove?.Invoke(this, data);

            // update Process state
            args.Process = args.Process && data.Process;
        }

        private void HotkeyHandler(object sender, KeyEventArgs args)
        {
            foreach (var hotkey in this.hotkeys.Where(e => e.Key == args.Key))
            {
                try
                {
                    hotkey.Execute(args);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        private void OnWndProc(WndEventArgs args)
        {
            switch (args.Msg)
            {
                case WM_MOUSEMOVE:
                    this.FireMouseMove(args);

                    break;

                case WM_MOUSEWHEEL:
                    var delta = (short)(args.WParam >> 16) & 0xFFFF;
                    this.MouseWheel?.Invoke(this, new MouseWheelEventArgs(delta));
                    break;

                case WM_LBUTTONUP:
                case WM_LBUTTONDOWN:
                case WM_LBUTTONDBLCLK:
                case WM_RBUTTONUP:
                case WM_RBUTTONDOWN:
                case WM_RBUTTONDBLCLK:
                    this.UpdateMouseButtons(args);
                    this.FireMouseClick(args);
                    break;

                case WM_KEYDOWN:
                    this.FireKeyDown(args);
                    break;

                case WM_KEYUP:
                    this.FireKeyUp(args);
                    break;
            }
        }

        private void UpdateMouseButtons(WndEventArgs args)
        {
            switch (args.Msg)
            {
                case WM_LBUTTONDOWN:
                    this.ActiveButtons |= MouseButtons.Left;
                    break;

                case WM_LBUTTONUP:
                    this.ActiveButtons &= ~MouseButtons.Left;
                    break;

                case WM_RBUTTONDOWN:
                    this.ActiveButtons |= MouseButtons.Right;
                    break;

                case WM_RBUTTONUP:
                    this.ActiveButtons &= ~MouseButtons.Right;
                    break;
            }
        }
    }
}