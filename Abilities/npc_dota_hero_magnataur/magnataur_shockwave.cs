// <copyright file="magnataur_shockwave.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_magnataur
{
    using Ensage.SDK.Extensions;

    public class magnataur_shockwave : LineAbility
    {
        public magnataur_shockwave(Ability ability)
            : base(ability)
        {
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("shock_width");
            }
        }

        public override float Speed
        {
            get
            {
                if (this.Owner.HasAghanimsScepter())
                {
                    return this.Ability.GetAbilitySpecialData("scepter_speed");
                }

                return this.Ability.GetAbilitySpecialData("shock_speed");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "shock_damage");
            }
        }
    }
}