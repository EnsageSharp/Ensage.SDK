// <copyright file="AttackOrbwalkingMode.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using Ensage.SDK.TargetSelector;
    using Ensage.SDK.TargetSelector.Modes;

    internal abstract class AttackOrbwalkingMode : AutoAttackMode
    {
        protected AttackOrbwalkingMode(IOrbwalker orbwalker, string name, uint key, bool hero, bool creep, bool neutral, bool building, bool deny, bool lasthit)
            : base(orbwalker)
        {
            this.Config = new AutoAttackModeConfig(orbwalker.Config.Factory, name, key, hero, creep, neutral, building, deny, lasthit);
            this.Selector = new AutoAttackModeSelector(this.Owner, this.Config);
        }

        public override bool CanExecute => this.Config.Key.Value.Active;

        private AutoAttackModeConfig Config { get; }

        private AutoAttackModeSelector Selector { get; }

        protected override Unit GetTarget()
        {
            return this.Selector.GetTarget();
        }
    }
}