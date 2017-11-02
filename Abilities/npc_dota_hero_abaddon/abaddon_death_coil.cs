// <copyright file="abaddon_death_coil.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_abaddon
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class abaddon_death_coil : RangedAbility, IHasHealthCost
    {
        public abaddon_death_coil(Ability ability)
            : base(ability)
        {
        }

        public float HealthCost
        {
            get
            {
                var cost = this.Ability.GetAbilitySpecialData("self_damage");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_abaddon_2);
                if (talent?.Level > 0)
                {
                    cost += talent.GetAbilitySpecialData("value");
                }

                return cost;
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("missile_speed");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("target_damage");
            }
        }
    }
}