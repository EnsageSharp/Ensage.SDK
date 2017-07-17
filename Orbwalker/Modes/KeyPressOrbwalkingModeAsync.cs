// <copyright file="KeyPressOrbwalkingModeAsync.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using System.Windows.Input;

    using Ensage.SDK.Input;

    public abstract class KeyPressOrbwalkingModeAsync : OrbwalkingModeAsync
    {
        private bool canExecute;

        protected KeyPressOrbwalkingModeAsync(IOrbwalkerManager orbwalker, IInputManager input, Key key)
            : base(orbwalker)
        {
            this.Input = input;
            this.Key = key;
        }

        public override bool CanExecute
        {
            get
            {
                return this.canExecute;
            }
        }

        public Key Key { get; set; }

        private IInputManager Input { get; }

        protected override void OnActivate()
        {
            base.OnActivate();
            this.Input.KeyUp += this.OnKeyUp;
            this.Input.KeyDown += this.KeyDown;
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
            this.Input.KeyUp -= this.OnKeyUp;
            this.Input.KeyDown -= this.KeyDown;
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == this.Key)
            {
                this.canExecute = true;
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