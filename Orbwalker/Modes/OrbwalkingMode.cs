// <copyright file="OrbwalkingMode.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using Ensage.SDK.Service;

    public abstract class OrbwalkingMode : ControllableService, IOrbwalkingMode
    {
        protected OrbwalkingMode(IServiceContext context)
        {
            this.Context = context;
            this.Owner = context.Owner;
            this.Orbwalker = context.Orbwalker;
        }

        public abstract bool CanExecute { get; }

        protected IServiceContext Context { get; }

        protected IOrbwalkerManager Orbwalker { get; }

        protected Unit Owner { get; }

        public abstract void Execute();
    }
}