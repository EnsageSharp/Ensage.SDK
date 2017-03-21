// <copyright file="queenofpain_sonic_wave.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_queenofpain
{
    using Ensage.SDK.Extensions;

    public class queenofpain_sonic_wave : ConeAbility
    {
        public queenofpain_sonic_wave(Ability ability)
            : base(ability)
        {
        }

        public override float EndWidth => this.Ability.GetAbilitySpecialData("final_aoe");

        public override float Speed => this.Ability.GetAbilitySpecialData("speed");

        public override float StartWidth => this.Ability.GetAbilitySpecialData("starting_aoe");
    }
}