// <copyright file="AutoAttackMode.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.SDK.TargetSelector;

    public abstract class AutoAttackModeAsync : OrbwalkingModeAsync
    {
        protected AutoAttackModeAsync(IOrbwalker orbwalker, ITargetSelectorManager targetSelector)
            : base(orbwalker)
        {
            this.TargetSelector = targetSelector;
            this.TargetSelector.Activate();
        }

        protected ITargetSelectorManager TargetSelector { get; }

        public override async Task ExecuteAsync(CancellationToken token)
        {
            this.Orbwalker.OrbwalkTo(this.GetTarget());
        }

        protected virtual Unit GetTarget()
        {
            return this.TargetSelector.Active?.GetTargets().FirstOrDefault();
        }
    }
}