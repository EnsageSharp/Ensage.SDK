// <copyright file="ControllableService.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using Ensage.Common.Menu;
    using Ensage.SDK.Menu;

    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Helper.Annotations;

    public abstract class ControllableService : IControllable
    {
        protected ControllableService([NotNull] MenuFactory factory, [NotNull] string name = "Active", bool @default = true, bool forceDisabled = false)
        {
            this.Active = factory.Item(name, @default);
            this.Active.Item.ValueChanged += this.OnActiveValueChanged;

            if (forceDisabled)
            {
                this.Active.Value = false;
            }

            if (this.Active.Value)
            {
                this.Activate();
            }
        }

        public MenuItem<bool> Active { get; }

        public bool IsActive { get; private set; }

        public void Activate()
        {
            if (this.IsActive)
            {
                return;
            }

            this.IsActive = true;
            this.OnActivate();
        }

        public void Deactivate()
        {
            if (!this.IsActive)
            {
                return;
            }

            this.IsActive = false;
            this.OnDeactivate();
        }

        protected virtual void OnActivate()
        {
        }

        protected virtual void OnDeactivate()
        {
        }

        private void OnActiveValueChanged(object sender, OnValueChangeEventArgs args)
        {
            if (args.GetNewValue<bool>())
            {
                this.Activate();
            }
            else
            {
                this.Deactivate();
            }
        }
    }
}