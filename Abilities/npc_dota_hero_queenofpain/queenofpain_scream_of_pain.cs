// <copyright file="queenofpain_scream_of_pain.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_queenofpain
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class queenofpain_scream_of_pain : AreaOfEffectAbility
    {
        public queenofpain_scream_of_pain(Ability ability)
            : base(ability)
        {
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("projectile_speed");
            }
        }

        protected override string RadiusName { get; } = "area_of_effect";

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.GetRawDamage();
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                // if (!this.CanAffectTarget(target)) // TODO
                // {
                // continue;
                // }
                var reduction = this.Ability.GetDamageReduction(target);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}