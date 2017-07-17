// <copyright file="OrbwalkingMode.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using Ensage.SDK.Service;

    public abstract class OrbwalkingMode : ControllableService, IOrbwalkingMode
    {
        protected OrbwalkingMode(IOrbwalkerManager orbwalker)
        {
            this.OrbwalkerManager = orbwalker;
        }

        public abstract bool CanExecute { get; }

        protected IServiceContext Context => this.Orbwalker.Context;

        protected IOrbwalker Orbwalker => this.OrbwalkerManager.Active;

        protected IOrbwalkerManager OrbwalkerManager { get; }

        protected Unit Owner => this.Context.Owner;

        public abstract void Execute();
    }
}