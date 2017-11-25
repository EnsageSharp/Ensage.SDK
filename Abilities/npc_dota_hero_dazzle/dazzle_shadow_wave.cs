// <copyright file="dazzle_shadow_wave.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_dazzle
{
    using System;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class dazzle_shadow_wave : AreaOfEffectAbility
    {
        public dazzle_shadow_wave(Ability ability)
            : base(ability)
        {
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_radius");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            var reduction = 0.0f;
            var target = targets.FirstOrDefault();
            if (target != null)
            {
                reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                var multiplier = EntityManager<Unit>.Entities.Count(x => x.IsValid && x.IsAlive && x.IsEnemy(target) && x.Distance2D(target) < this.Radius);
                damage *= Math.Max(0, Math.Min(multiplier, this.Ability.GetAbilitySpecialData("max_targets")));
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }
    }
}