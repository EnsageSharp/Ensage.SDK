// <copyright file="elder_titan_echo_stomp.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_elder_titan
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class elder_titan_echo_stomp : ActiveAbility, IChannable, IHasTargetModifier, IAreaOfEffectAbility
    {
        public elder_titan_echo_stomp(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public float ChannelDuration
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

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("sleep_duration");
            }
        }

        public bool IsChanneling
        {
            get
            {
                return this.Ability.IsChanneling;
            }
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
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

                return this.ChannelDuration - this.Ability.ChannelTime;
            }
        }

        public string TargetModifierName { get; } = "modifier_elder_titan_echo_stomp";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "stomp_damage");
            }
        }
    }
}