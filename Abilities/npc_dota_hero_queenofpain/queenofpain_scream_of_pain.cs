// <copyright file="queenofpain_scream_of_pain.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_queenofpain
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class queenofpain_scream_of_pain : CircleAbility
    {
        public queenofpain_scream_of_pain(Ability ability)
            : base(ability)
        {
        }

        public override float Radius => this.Ability.GetAbilitySpecialData("area_of_effect");

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = base.GetDamage(targets);
            var amplify = this.Ability.SpellAmplification();
            foreach (var target in targets)
            {
                if (!this.CanAffectTarget(target))
                {
                    continue;
                }

                var reduction = this.Ability.GetDamageReduction(target);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}