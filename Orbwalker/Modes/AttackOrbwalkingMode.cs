// <copyright file="AttackOrbwalkingMode.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using Ensage.SDK.Orbwalker.Config;
    using Ensage.SDK.TargetSelector;
    using Ensage.SDK.TargetSelector.Modes;

    internal abstract class AttackOrbwalkingMode : AutoAttackMode
    {
        private readonly bool building;

        private readonly bool creep;

        private readonly bool deny;

        private readonly bool hero;

        private readonly uint key;

        private readonly bool lasthit;

        private readonly string name;

        private readonly bool neutral;

        protected AttackOrbwalkingMode(
            IOrbwalker orbwalker,
            ITargetSelectorManager ts,
            string name,
            uint key,
            bool hero,
            bool creep,
            bool neutral,
            bool building,
            bool deny,
            bool lasthit)
            : base(orbwalker, ts)
        {
            this.key = key;
            this.hero = hero;
            this.creep = creep;
            this.neutral = neutral;
            this.building = building;
            this.deny = deny;
            this.lasthit = lasthit;
            this.name = name;
        }

        public override bool CanExecute
        {
            get
            {
                if (this.Config == null)
                {
                    return false;
                }

                return this.Config.Active && this.Config.Key;
            }
        }

        private AutoAttackModeConfig Config { get; set; }

        private AutoAttackModeSelector Selector { get; set; }

        protected override Unit GetTarget()
        {
            return this.Selector.GetTarget();
        }

        protected override void OnActivate()
        {
            this.Config = new AutoAttackModeConfig(this.Orbwalker.Settings.Factory.Parent, this.name, this.key, this.hero, this.creep, this.neutral, this.building, this.deny, this.lasthit);
            this.Selector = new AutoAttackModeSelector(this.Owner, this.TargetSelector, this.Config);

            base.OnActivate();
        }

        protected override void OnDeactivate()
        {
            this.Config?.Dispose();
            base.OnDeactivate();
        }
    }
}