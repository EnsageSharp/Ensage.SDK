// <copyright file="AutoAttackMode.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using System.Linq;

    using Ensage.SDK.TargetSelector;

    public abstract class AutoAttackMode : OrbwalkingMode
    {
        protected AutoAttackMode(IOrbwalkerManager orbwalker, ITargetSelectorManager targetSelector)
            : base(orbwalker)
        {
            this.TargetSelector = targetSelector;
            this.TargetSelector.Activate();
        }

        protected ITargetSelectorManager TargetSelector { get; }

        public override void Execute()
        {
            this.Orbwalker.OrbwalkTo(this.GetTarget());
        }

        protected virtual Unit GetTarget()
        {
            return this.TargetSelector.Active?.GetTargets().FirstOrDefault();
        }
    }
}