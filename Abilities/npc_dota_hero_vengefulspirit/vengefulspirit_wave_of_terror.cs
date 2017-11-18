// <copyright file="vengefulspirit_wave_of_terror.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_vengefulspirit
{
    using Ensage.SDK.Extensions;

    public class vengefulspirit_wave_of_terror : LineAbility
    {
        public vengefulspirit_wave_of_terror(Ability ability)
            : base(ability)
        {
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("wave_width");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("wave_speed");
            }
        }
    }
}