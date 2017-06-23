// <copyright file="queenofpain_sonic_wave.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_queenofpain
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

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

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData(this.Owner.HasAghanimsScepter() ? "damage_scepter" : "damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}