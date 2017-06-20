// <copyright file="skywrath_mage_mystic_flare.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_skywrath_mage
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class skywrath_mage_mystic_flare : AreaOfEffectAbility, IHasDot
    {
        public skywrath_mage_mystic_flare(Ability ability)
            : base(ability)
        {
        }

        public float Duration
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
                return (this.Ability.GetAbilitySpecialData("damage") / this.Duration) * this.TickRate;
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

        public override float GetDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets);
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Ability.SpellAmplification();
            var reduction = 0.0f;
            if (targets.Any())
            {
                //add dmg reduction based on targets.count ?
                reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets) * (this.Duration / this.TickRate);
        }
    }
}