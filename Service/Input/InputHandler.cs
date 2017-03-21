// <copyright file="InputHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Input
{
    using System;
    using System.ComponentModel.Composition;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Forms;
    using System.Windows.Input;

    [Export(typeof(IInput))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "StyleCop.SA1310")]
    public class InputHandler : IInput, IDisposable
    {
        private const uint WM_KEYDOWN = 0x0100;

        private const uint WM_KEYUP = 0x0101;

        private const uint WM_LBUTTONDBLCLK = 0x0203;

        private const uint WM_LBUTTONDOWN = 0x0201;

        private const uint WM_LBUTTONUP = 0x0202;

        private const uint WM_MOUSEMOVE = 0x0200;

        private const uint WM_RBUTTONDBLCLK = 0x0206;

        private const uint WM_RBUTTONDOWN = 0x0204;

        private const uint WM_RBUTTONUP = 0x0205;

        private bool disposed;

        public InputHandler()
        {
            Game.OnWndProc += this.OnWndProc;
        }

        public event EventHandler<KeyEventArgs> KeyDown;

        public event EventHandler<KeyEventArgs> KeyUp;

        public event EventHandler<MouseEventArgs> MouseClick;

        public event EventHandler<MouseEventArgs> MouseMove;

        public MouseButtons ActiveButtons { get; private set; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool IsKeyDown(Key key)
        {
            return Game.IsKeyDown(key);
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
            var data = new KeyEventArgs((uint)args.WParam, args.Process);
            this.KeyDown?.Invoke(this, data);

            // update Process state
            args.Process = args.Process && data.Process;
        }

        private void FireKeyUp(WndEventArgs args)
        {
            var data = new KeyEventArgs((uint)args.WParam, args.Process);
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

        private void OnWndProc(WndEventArgs args)
        {
            switch (args.Msg)
            {
                case WM_MOUSEMOVE:
                    this.FireMouseMove(args);
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