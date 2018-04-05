// <copyright file="MenuInputManager.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Input;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.Config;
    using Ensage.SDK.Menu.Items;
    using Ensage.SDK.Service;

    using PlaySharp.Toolkit.Helper.Annotations;
    using NLog;

    [Flags]
    public enum HotkeyFlags
    {
        /// <summary>
        ///     Triggers the callback once, when the registered <see cref="MenuHotkey" /> is pressed.
        ///     This event exists only for <see cref="Key" />s and not for <see cref="MouseButtons" />.
        /// </summary>
        Press = 1 << 0,

        /// <summary>
        ///     Calls the callback as long as the registered <see cref="MenuHotkey" /> is pressed.
        ///     The <see cref="Down" /> event is only called once for <see cref="MouseButtons" /> (like <see cref="Press" /> for
        ///     <see cref="Key" />s).
        /// </summary>
        Down = 1 << 1,

        /// <summary>
        ///     Calls the callback once when the registered <see cref="MenuHotkey" /> is released.
        /// </summary>
        Up = 1 << 2
    }

    public sealed class MenuInputEventArgs : EventArgs
    {
        public MenuInputEventArgs(Key key, HotkeyFlags flag)
        {
            this.Key = key;
            this.Flag = flag;
            this.MouseButton = MouseButtons.None;
        }

        public MenuInputEventArgs(MouseButtons mouseButton, HotkeyFlags flag)
        {
            this.Key = Key.None;
            this.MouseButton = mouseButton;
            this.Flag = flag;
        }

        public HotkeyFlags Flag { get; }

        public bool Handled { get; set; } = false;

        public Key Key { get; }

        public MouseButtons MouseButton { get; }
    }

    public sealed class MenuHotkey
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly Action<MenuInputEventArgs> action;

        public MenuHotkey(KeyOrMouseButton key, [NotNull] Action<MenuInputEventArgs> action, HotkeyFlags flags)
        {
            this.Flags = flags;
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.Hotkey = key;
            this.Name = Guid.NewGuid().ToString();
        }

        public HotkeyFlags Flags { get; set; }

        public KeyOrMouseButton Hotkey { get; set; }

        public string Name { get; }

        public void Execute(MenuInputEventArgs args)
        {
            // Log.Debug($"Hotkey: {this.Name} {this.Key}");
            this.action(args);
        }
    }

    [Export]
    public sealed class MenuInputManager : ControllableService
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public readonly IInputManager InputManager;

        private readonly GeneralConfig GeneralConfig;

        private readonly List<MenuHotkey> hotkeys = new List<MenuHotkey>();

        private readonly Dictionary<Key, bool> keyDownStates = new Dictionary<Key, bool>();

        [ImportingConstructor]
        public MenuInputManager([Import] IInputManager inputManager, [Import] MenuManager menuManager)
        {
            this.InputManager = inputManager;
            this.GeneralConfig = menuManager.MenuConfig.GeneralConfig;
        }

        public MenuHotkey RegisterHotkey(KeyOrMouseButton key, Action<MenuInputEventArgs> action, HotkeyFlags flags = HotkeyFlags.Press)
        {
            var hotkey = new MenuHotkey(key, action, flags);
            this.hotkeys.Add(hotkey);

            return hotkey;
        }

        public void UnregisterHotkey(string name)
        {
            this.hotkeys.RemoveAll(x => x.Name == name);
        }

        public void UnregisterHotkey(MenuHotkey hotkey)
        {
            this.UnregisterHotkey(hotkey.Name);
        }

        protected override void OnActivate()
        {
            this.InputManager.KeyDown += this.KeyDown;
            this.InputManager.KeyUp += this.KeyUp;
            this.InputManager.MouseClick += this.MouseClick;
        }

        protected override void OnDeactivate()
        {
            this.InputManager.KeyDown -= this.KeyDown;
            this.InputManager.KeyUp -= this.KeyUp;
            this.InputManager.MouseClick -= this.MouseClick;
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            var menuHotkeys = this.hotkeys.Where(x => x.Hotkey.Key == e.Key).ToList();
            if (menuHotkeys.Any())
            {
                if (!this.keyDownStates.TryGetValue(e.Key, out var state) || state == false)
                {
                    var pressArgs = new MenuInputEventArgs(e.Key, HotkeyFlags.Press);
                    foreach (var menuHotkey in menuHotkeys.Where(x => (x.Flags & HotkeyFlags.Press) == HotkeyFlags.Press))
                    {
                        menuHotkey.Execute(pressArgs);
                        if (pressArgs.Handled)
                        {
                            break;
                        }
                    }
                }

                var downArgs = new MenuInputEventArgs(e.Key, HotkeyFlags.Down);
                foreach (var menuHotkey in menuHotkeys.Where(x => (x.Flags & HotkeyFlags.Down) == HotkeyFlags.Down))
                {
                    menuHotkey.Execute(downArgs);
                    if (downArgs.Handled)
                    {
                        break;
                    }
                }

                this.BlockKeys(e);
            }

            this.keyDownStates[e.Key] = true;
        }

        private void KeyUp(object sender, KeyEventArgs e)
        {
            this.keyDownStates[e.Key] = false;

            var pressArgs = new MenuInputEventArgs(e.Key, HotkeyFlags.Up);
            foreach (var menuHotkey in this.hotkeys.Where(x => x.Hotkey.Key == e.Key && (x.Flags & HotkeyFlags.Up) == HotkeyFlags.Up))
            {
                this.BlockKeys(e);
                menuHotkey.Execute(pressArgs);
                if (pressArgs.Handled)
                {
                    break;
                }
            }
        }

        private void MouseClick(object sender, MouseEventArgs e)
        {
            MouseButtons button;
            bool downEvent;

            if ((e.Buttons & MouseButtons.LeftDown) == MouseButtons.LeftDown)
            {
                button = MouseButtons.Left;
                downEvent = true;
            }
            else if ((e.Buttons & MouseButtons.RightDown) == MouseButtons.RightDown)
            {
                button = MouseButtons.Right;
                downEvent = true;
            }
            else if ((e.Buttons & MouseButtons.XButton1Down) == MouseButtons.XButton1Down)
            {
                button = MouseButtons.XButton1;
                downEvent = true;
            }
            else if ((e.Buttons & MouseButtons.XButton2Down) == MouseButtons.XButton2Down)
            {
                button = MouseButtons.XButton2;
                downEvent = true;
            }
            else if ((e.Buttons & MouseButtons.LeftUp) == MouseButtons.LeftUp)
            {
                button = MouseButtons.Left;
                downEvent = false;
            }
            else if ((e.Buttons & MouseButtons.RightUp) == MouseButtons.RightUp)
            {
                button = MouseButtons.Right;
                downEvent = false;
            }
            else if ((e.Buttons & MouseButtons.XButton1Up) == MouseButtons.XButton1Up)
            {
                button = MouseButtons.XButton1;
                downEvent = false;
            }
            else if ((e.Buttons & MouseButtons.XButton2Up) == MouseButtons.XButton2Up)
            {
                button = MouseButtons.XButton1;
                downEvent = false;
            }
            else
            {
                Log.Debug($"No button pressed in MouseClick event: {e.Buttons} ({e.Clicks})");
                return;
            }

            var menuHotkeys = this.hotkeys.Where(x => x.Hotkey.MouseButton == button).ToList();
            if (menuHotkeys.Any())
            {
                var flag = downEvent ? HotkeyFlags.Down : HotkeyFlags.Up;
                var buttonArgs = new MenuInputEventArgs(button, flag);
                foreach (var menuHotkey in menuHotkeys.Where(x => (x.Flags & flag) == flag))
                {
                    menuHotkey.Execute(buttonArgs);
                    if (buttonArgs.Handled)
                    {
                        break;
                    }
                }
            }
        }

        private void BlockKeys(KeyEventArgs e)
        {
            if (!this.GeneralConfig.BlockKeys || Game.IsChatOpen)
            {
                return;
            }

            e.Process = false;
        }
    }
}