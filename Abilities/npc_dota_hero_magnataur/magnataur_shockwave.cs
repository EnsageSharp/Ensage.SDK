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

        protected override string RadiusName { get; } = "shock_width";

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("shock_damage");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_magnus_4);
                if (talent?.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }

        protected override string SpeedName
        {
            get
            {
                return this.Owner.HasAghanimsScepter() ? "scepter_speed" : "shock_speed";
            }
        }
    }
}