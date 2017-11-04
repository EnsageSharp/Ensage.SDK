// <copyright file="pangolier_gyroshell.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_pangolier
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class pangolier_gyroshell : ActiveAbility, IChannable, IAreaOfEffectAbility, IHasModifier, IHasTargetModifier
    {
        public pangolier_gyroshell(Ability ability)
            : base(ability)
        {
            var stopAbility = this.Owner.GetAbilityById(AbilityId.pangolier_gyroshell_stop);
            this.GyroshellStopAbility = new pangolier_gyroshell_stop(stopAbility);
        }

        public float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("cast_time_tooltip");
            }
        }

        public pangolier_gyroshell_stop GyroshellStopAbility { get; }

        public bool IsChanneling
        {
            get
            {
                return this.Ability.IsChanneling;
            }
        }

        public string ModifierName { get; } = "modifier_pangolier_gyroshell";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("hit_radius");
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

        public string TargetModifierName { get; } = "modifier_pangolier_gyroshell_stunned";
    }
}