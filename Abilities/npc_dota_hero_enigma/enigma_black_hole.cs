// <copyright file="enigma_black_hole.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_enigma
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class enigma_black_hole : CircleAbility, IHasDot
    {
        public enigma_black_hole(Ability ability)
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

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("pull_radius");
            }
        }

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("far_damage");
            }
        }

        public string TargetModifierName { get; } = "modifier_enigma_black_hole_pull";

        public float TickRate
        {
            get
            {
                return 1.0f;
                // wrong tick rate in special data
                // return this.Ability.GetAbilitySpecialData("tick_rate");
            }
        }

        public override float GetDamage(params Unit[] target)
        {
            return this.GetTickDamage(target);
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Ability.SpellAmplification();
            if (!targets.Any())
            {
                return DamageHelpers.GetSpellDamage(damage, amplify);
            }

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        public float GetTotalDamage(params Unit[] target)
        {
            return this.GetTickDamage(target) * (this.Duration / this.TickRate);
        }
    }
}