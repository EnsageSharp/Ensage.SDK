// <copyright file="skywrath_mage_mystic_flare.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_skywrath_mage
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class skywrath_mage_mystic_flare : CircleAbility, IHasDot
    {
        public skywrath_mage_mystic_flare(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public bool HasInitialDamage { get; } = false;

        public float RawTickDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("damage");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_skywrath_5);
                if (talent?.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return (damage / this.DamageDuration) * this.TickRate;
            }
        }

        public string TargetModifierName { get; } = "modifier_skywrath_mystic_flare_aura_effect";

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_interval");
            }
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Ability.SpellAmplification();
            if (!targets.Any())
            {
                return DamageHelpers.GetSpellDamage(damage, amplify);
            }

            damage /= targets.Length;

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate);
        }
    }
}