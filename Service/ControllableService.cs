// <copyright file="ControllableService.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using Ensage.Common;

    using PlaySharp.Toolkit.Helper;

    public abstract class ControllableService : IControllable
    {
        protected ControllableService(bool activateOnCreation = true)
        {
            if (activateOnCreation)
            {
                DelayAction.Add(0, this.Activate);
            }
        }

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
    }
}