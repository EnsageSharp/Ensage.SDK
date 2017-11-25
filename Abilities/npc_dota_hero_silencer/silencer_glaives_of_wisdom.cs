// <copyright file="silencer_glaives_of_wisdom.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_silencer
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class silencer_glaives_of_wisdom : OrbAbility
    {
        public silencer_glaives_of_wisdom(Ability ability)
            : base(ability)
        {
        }

        public override SpellPierceImmunityType PiercesSpellImmunity
        {
            get
            {
                if (this.Owner.HasAghanimsScepter())
                {
                    return SpellPierceImmunityType.EnemiesYes;
                }

                return base.PiercesSpellImmunity;
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = base.GetDamage(targets);
            var spellAmp = this.Owner.GetSpellAmplification();

            var damagePercent = this.Ability.GetAbilitySpecialData("intellect_damage_pct") / 100.0f;

            var hero = this.Owner as Hero;
            var bonusDamage = (hero?.TotalIntelligence ?? 0) * damagePercent;

            var isSilenced = targets.First().IsSilenced();
            if (isSilenced && this.Owner.HasAghanimsScepter())
            {
                bonusDamage *= 2;
            }

            var reduction = 0.0f;
            if (targets.Any())
            {
                var target = targets.First();
                reduction = this.Ability.GetDamageReduction(target, this.DamageType);
            }

            damage += DamageHelpers.GetSpellDamage(bonusDamage, spellAmp, reduction);
            return damage;
        }
    }
}