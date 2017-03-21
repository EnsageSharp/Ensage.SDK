// <copyright file="vengefulspirit_wave_of_terror.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_vengefulspirit
{
    using System;

    using Ensage.SDK.Extensions;

    public class vengefulspirit_wave_of_terror : LineAbility
    {
        public vengefulspirit_wave_of_terror(Ability ability)
            : base(ability)
        {
        }

        public override float Speed => this.Ability.GetAbilitySpecialData("wave_speed");

        public override float Width => this.Ability.GetAbilitySpecialData("wave_width");

        public override float GetDamage(params Unit[] target)
        {
            throw new NotImplementedException();
        }
    }
}