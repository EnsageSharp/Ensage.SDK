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

        public override float Range
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("distance");
            }
        }

        protected override string EndRadiusName { get; } = "final_aoe";

        protected override string RadiusName { get; } = "starting_aoe";

        // TODO: GetDamage
    }
}