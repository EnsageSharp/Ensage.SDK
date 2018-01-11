// <copyright file="HotkeySelector.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Items
{
    using System;
    using System.ComponentModel.Composition;
    using System.Windows.Input;

    using Ensage.SDK.Input;
    using Ensage.SDK.Menu.ValueBinding;
    using Ensage.SDK.Service;

    using Newtonsoft.Json;

    public class KeyOrMouseButton : ICloneable
    {
        [JsonIgnore]
        private Key key = Key.None;

        [JsonIgnore]
        private MouseButtons mouseButton = MouseButtons.None;

        public KeyOrMouseButton()
        {
        }

        public KeyOrMouseButton(Key key)
        {
            this.key = key;
        }

        public KeyOrMouseButton(MouseButtons mouseButton)
        {
            this.mouseButton = mouseButton;
        }

        public event EventHandler<ValueChangingEventArgs<KeyOrMouseButton>> ValueChanging;

        public Key Key
        {
            get
            {
                return this.key;
            }

            set
            {
                if (value == this.key)
                {
                    return;
                }

                if (this.OnValueChanged(new KeyOrMouseButton(value)))
                {
                    if (value != Key.None)
                    {
                        this.mouseButton = MouseButtons.None;
                    }

                    this.key = value;
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
                if (value == this.mouseButton)
                {
                    return;
                }

                var newButton = value & MouseButtons.HighestNoUpDownFlag;
                if (this.OnValueChanged(new KeyOrMouseButton(newButton)))
                {
                    if (value != MouseButtons.None)
                    {
                        this.key = Key.None;
                    }

                    this.mouseButton = newButton;
                }
            }
        }

        public static bool operator ==(KeyOrMouseButton o1, KeyOrMouseButton o2)
        {
            return o1.Key == o2.Key && o1.MouseButton == o2.MouseButton;
        }

        public static bool operator !=(KeyOrMouseButton o1, KeyOrMouseButton o2)
        {
            return !(o1 == o2);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            return obj is KeyOrMouseButton button && this == button;
        }

        public override int GetHashCode()
        {
            return this.Key.GetHashCode() ^ this.MouseButton.GetHashCode();
        }

        public override string ToString()
        {
            return this.MouseButton != MouseButtons.None ? $"Mouse {this.MouseButton.ToString()}" : this.Key.ToString();
        }

        protected virtual bool OnValueChanged(KeyOrMouseButton newValue)
        {
            var args = new ValueChangingEventArgs<KeyOrMouseButton>(newValue, this);
            this.ValueChanging?.Invoke(this, args);
            return args.Process;
        }
    }

    public class HotkeySelector : ControllableService, ILoadable, ICloneable
    {
        [JsonIgnore]
        private HotkeyFlags flags;

        [JsonIgnore]
        private MenuHotkey hotkey;

        public HotkeySelector()
            : base(false)
        {
        }

        public HotkeySelector(MouseButtons mouseButton, HotkeyFlags flags = HotkeyFlags.Press)
            : base(false)
        {
            this.Hotkey = new KeyOrMouseButton(mouseButton);
            this.flags = flags;
        }

        public HotkeySelector(Key key, HotkeyFlags flags = HotkeyFlags.Press)
            : base(false)
        {
            this.Hotkey = new KeyOrMouseButton(key);
            this.flags = flags;
        }

        public HotkeySelector(MouseButtons mouseButton, Action<MenuInputEventArgs> action, HotkeyFlags flags = HotkeyFlags.Press)
            : base(false)
        {
            this.Hotkey = new KeyOrMouseButton(mouseButton);
            this.Action = action;
            this.flags = flags;
        }

        public HotkeySelector(Key key, Action<MenuInputEventArgs> action, HotkeyFlags flags = HotkeyFlags.Press)
            : base(false)
        {
            this.Hotkey = new KeyOrMouseButton(key);
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

        public KeyOrMouseButton Hotkey { get; set; }

        [Import]
        [JsonIgnore]
        public MenuInputManager InputManager { get; set; }

        [JsonIgnore]
        public bool IsAssigningNewHotkey { get; private set; }

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

        public object Clone()
        {
            var result = (HotkeySelector)this.MemberwiseClone();
            result.Hotkey = (KeyOrMouseButton)this.Hotkey.Clone();
            return result;
        }

        public bool Load(object data)
        {
            var other = (HotkeySelector)data;
            if (this.Hotkey != other.Hotkey)
            {
                this.Hotkey = other.Hotkey;
                if (this.hotkey != null)
                {
                    this.hotkey.Hotkey = this.Hotkey;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return this.IsAssigningNewHotkey ? "<?>" : this.Hotkey.ToString();
        }

        protected override void OnActivate()
        {
            this.Hotkey.ValueChanging += this.HotkeyValueChanging;
            this.hotkey = this.InputManager.RegisterHotkey(this.Hotkey, this.ExecuteAction, this.Flags);
        }

        protected override void OnDeactivate()
        {
            this.Hotkey.ValueChanging -= this.HotkeyValueChanging;
            if (this.hotkey != null)
            {
                this.InputManager.UnregisterHotkey(this.hotkey);
            }
        }

        private void ExecuteAction(MenuInputEventArgs args)
        {
            if (!this.IsAssigningNewHotkey)
            {
                this.Action?.Invoke(args);
            }
        }

        private void HotkeyValueChanging(object sender, ValueChangingEventArgs<KeyOrMouseButton> e)
        {
            if (this.IsAssigningNewHotkey)
            {
                e.Process = false;
            }
        }

        private void NextKeyDown(object sender, KeyEventArgs e)
        {
            this.InputManager.InputManager.KeyDown -= this.NextKeyDown;
            this.InputManager.InputManager.MouseClick -= this.NextMouseClick;
            this.IsAssigningNewHotkey = false;

            if (e.Key != Key.Escape)
            {
                this.Hotkey.Key = e.Key;
            }
        }

        private void NextMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Buttons == MouseButtons.LeftDown || e.Buttons == MouseButtons.RightDown || e.Buttons == MouseButtons.XButton1Down || e.Buttons == MouseButtons.XButton2Down)
            {
                this.InputManager.InputManager.KeyDown -= this.NextKeyDown;
                this.InputManager.InputManager.MouseClick -= this.NextMouseClick;
                this.IsAssigningNewHotkey = false;

                this.Hotkey.MouseButton = e.Buttons;
            }
        }
    }
}