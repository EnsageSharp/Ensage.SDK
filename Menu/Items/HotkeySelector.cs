// <copyright file="HotkeySelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    using System;
    using System.ComponentModel.Composition;
    using System.Windows.Input;

    using Ensage.SDK.Input;
    using Ensage.SDK.Service;

    using Newtonsoft.Json;

    public class HotkeySelector : ControllableService, ILoadable, ICloneable
    {
        [JsonIgnore]
        private HotkeyFlags flags;

        [JsonIgnore]
        private MenuHotkey hotkey;

        [JsonIgnore]
        private Key key = Key.None;

        [JsonIgnore]
        private MouseButtons mouseButton = MouseButtons.None;

        public HotkeySelector()
        {
        }

        public HotkeySelector(MouseButtons mouseButton, HotkeyFlags flags = HotkeyFlags.Press)
        {
            this.mouseButton = mouseButton;
            this.flags = flags;
        }

        public HotkeySelector(Key key, HotkeyFlags flags = HotkeyFlags.Press)
        {
            this.key = key;
            this.flags = flags;
        }

        public HotkeySelector(MouseButtons mouseButton, Action<MenuInputEventArgs> action, HotkeyFlags flags = HotkeyFlags.Press)
        {
            this.mouseButton = mouseButton;
            this.Action = action;
            this.flags = flags;
        }

        public HotkeySelector(Key key, Action<MenuInputEventArgs> action, HotkeyFlags flags = HotkeyFlags.Press)
        {
            this.key = key;
            this.Action = action;
            this.flags = flags;
        }

        [JsonIgnore]
        public Action<MenuInputEventArgs> Action { get; set; }

        [JsonIgnore]
        public HotkeyFlags Flags
        {
            get
            {
                return this.flags;
            }

            set
            {
                this.flags = value;
                if (this.hotkey != null)
                {
                    this.hotkey.Flags = value;
                }
            }
        }

        [Import]
        [JsonIgnore]
        public MenuInputManager InputManager { get; set; }

        [JsonIgnore]
        public bool IsAssigningNewHotkey { get; private set; }

        public Key Key
        {
            get
            {
                if (this.hotkey != null)
                {
                    return this.hotkey.Key;
                }

                return this.key;
            }

            set
            {
                this.key = value;
                if (this.hotkey != null)
                {
                    this.hotkey.Key = value;
                }
            }
        }

        public MouseButtons MouseButton
        {
            get
            {
                if (this.hotkey != null)
                {
                    return this.hotkey.MouseButton;
                }

                return this.mouseButton;
            }

            set
            {
                this.mouseButton = value & MouseButtons.HighestNoUpDownFlag;
                if (this.hotkey != null)
                {
                    this.hotkey.MouseButton = this.mouseButton;
                }
            }
        }

        public void AssignNewHotkey()
        {
            if (this.IsAssigningNewHotkey)
            {
                return;
            }

            this.IsAssigningNewHotkey = true;
            this.InputManager.InputManager.KeyDown += this.NextKeyDown;
            this.InputManager.InputManager.MouseClick += this.NextMouseClick;
        }

        public bool Load(object data)
        {
            var other = (HotkeySelector)data;
            if (this.Key != other.Key || this.MouseButton != other.MouseButton)
            {
                this.Key = other.Key;
                this.MouseButton = other.MouseButton;
            }

            return false;
        }

        public override string ToString()
        {
            if (this.IsAssigningNewHotkey)
            {
                return "<?>";
            }

            return this.MouseButton != MouseButtons.None ? $"Mouse {this.MouseButton.ToString()}" : this.key.ToString();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        protected override void OnActivate()
        {
            if (this.MouseButton != MouseButtons.None)
            {
                this.hotkey = this.InputManager?.RegisterHotkey(this.MouseButton, this.ExecuteAction, this.Flags);
            }
            else
            {
                this.hotkey = this.InputManager?.RegisterHotkey(this.Key, this.ExecuteAction, this.Flags);
            }
        }

        protected override void OnDeactivate()
        {
            if (this.hotkey != null)
            {
                this.InputManager?.UnregisterHotkey(this.hotkey);
            }
        }

        private void ExecuteAction(MenuInputEventArgs args)
        {
            if (!this.IsAssigningNewHotkey)
            {
                this.Action?.Invoke(args);
            }
        }

        private void NextKeyDown(object sender, KeyEventArgs e)
        {
            this.InputManager.InputManager.KeyDown -= this.NextKeyDown;
            this.InputManager.InputManager.MouseClick -= this.NextMouseClick;
            this.IsAssigningNewHotkey = false;

            if (e.Key != Key.Escape)
            {
                this.Key = e.Key;
            }
        }

        private void NextMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Buttons == MouseButtons.LeftDown || e.Buttons == MouseButtons.RightDown || e.Buttons == MouseButtons.XButton1Down || e.Buttons == MouseButtons.XButton2Down)
            {
                this.InputManager.InputManager.KeyDown -= this.NextKeyDown;
                this.InputManager.InputManager.MouseClick -= this.NextMouseClick;
                this.IsAssigningNewHotkey = false;

                this.MouseButton = e.Buttons;
            }
        }
    }
}