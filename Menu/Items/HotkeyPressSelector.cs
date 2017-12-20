// <copyright file="HotkeyPressSelector.cs" company="Ensage">
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

    public class HotkeyPressSelector : ControllableService, ILoadable
    {
        [JsonIgnore]
        private HotkeyFlags flags;

        [JsonIgnore]
        private MenuHotkey hotkey;

        [JsonIgnore]
        private Key key = Key.None;

        [JsonIgnore]
        private MouseButtons mouseButton = MouseButtons.None;

        public HotkeyPressSelector()
        {
        }

        public HotkeyPressSelector(MouseButtons mouseButton, Action<MenuInputEventArgs> action, HotkeyFlags flags = HotkeyFlags.Press)
        {
            this.mouseButton = mouseButton;
            this.Action = action;
            this.flags = flags;
        }

        public HotkeyPressSelector(Key key, Action<MenuInputEventArgs> action, HotkeyFlags flags = HotkeyFlags.Press)
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

        public Key Key
        {
            get
            {
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
                return this.mouseButton;
            }

            set
            {
                this.mouseButton = value;
                if (this.hotkey != null)
                {
                    this.hotkey.MouseButton = value;
                }
            }
        }

        public bool Load(object data)
        {
            var other = (HotkeyPressSelector)data;
            if (this.Key != other.Key)
            {
                this.Key = other.Key;
            }

            return false;
        }

        public override string ToString()
        {
            return this.MouseButton != MouseButtons.None ? this.MouseButton.ToString() : this.key.ToString();
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
            this.Action?.Invoke(args);
        }
    }
}