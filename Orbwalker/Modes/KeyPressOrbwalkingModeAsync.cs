// <copyright file="KeyPressOrbwalkingModeAsync.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using System.ComponentModel;
    using System.Windows.Input;

    using Ensage.Common.Menu;
    using Ensage.SDK.Input;
    using Ensage.SDK.Menu;
    using Ensage.SDK.Service;

    public abstract class KeyPressOrbwalkingModeAsync : OrbwalkingModeAsync
    {
        private bool canExecute;

        protected KeyPressOrbwalkingModeAsync(IServiceContext context, Key key)
            : base(context)
        {
            this.Input = context.Input;
            this.Key = key;
        }

        protected KeyPressOrbwalkingModeAsync(IServiceContext context, MenuItem<KeyBind> key)
            : base(context)
        {
            this.MenuKey = key;
        }

        public override bool CanExecute
        {
            get
            {
                return this.canExecute;
            }
        }

        public Key Key { get; set; }

        public MenuItem<KeyBind> MenuKey { get; }

        private IInputManager Input { get; }

        protected override void OnActivate()
        {
            base.OnActivate();
            if (this.MenuKey != null)
            {
                this.MenuKey.PropertyChanged += this.MenuKeyOnPropertyChanged;
            }
            else
            {
                this.Input.KeyUp += this.OnKeyUp;
                this.Input.KeyDown += this.KeyDown;
            }
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
            if (this.MenuKey != null)
            {
                this.MenuKey.PropertyChanged -= this.MenuKeyOnPropertyChanged;
            }
            else
            {
                this.Input.KeyUp -= this.OnKeyUp;
                this.Input.KeyDown -= this.KeyDown;
            }
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == this.Key)
            {
                this.canExecute = true;
            }
        }

        private void MenuKeyOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (this.MenuKey)
            {
                this.canExecute = true;
            }
            else
            {
                this.canExecute = false;
                this.Cancel();
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == this.Key)
            {
                this.canExecute = false;
                this.Cancel();
            }
        }
    }
}