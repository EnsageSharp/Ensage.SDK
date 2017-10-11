// <copyright file="warlock_upheaval.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_warlock
{
    using Ensage.SDK.Abilities.Components;

    public class warlock_upheaval : AreaOfEffectAbility, IChannable, IHasTargetModifier
    {
        public warlock_upheaval(Ability ability)
            : base(ability)
        {
        }

        public float Duration
        {
            get
            {
                var level = this.Ability.Level;
                if (level == 0)
                {
                    return 0;
                }

                return this.Ability.GetChannelTime(level - 1);
            }
        }

        public bool IsChanneling
        {
            get
            {
                return this.Ability.IsChanneling;
            }
        }

        public float RemainingDuration
        {
            get
            {
                if (!this.IsChanneling)
                {
                    return 0;
                }

                return this.Duration - this.Ability.ChannelTime;
            }
        }

        public string TargetModifierName { get; } = "modifier_warlock_upheaval";

        protected override string RadiusName { get; } = "aoe";
    }
}