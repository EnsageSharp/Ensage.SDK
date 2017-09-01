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

        protected override string SpeedName
        {
            get
            {
                return this.Owner.HasAghanimsScepter() ? "scepter_speed" : "shock_speed";
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("shock_damage");
            }
        }
    }
}