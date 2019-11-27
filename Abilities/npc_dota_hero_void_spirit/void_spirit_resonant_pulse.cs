// <copyright file="void_spirit_resonant_pulse.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_void_spirit
{
    using System;
    using System.Linq;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Extensions;

    using SharpDX;

    public class void_spirit_resonant_pulse : ActiveAbility, IAreaOfEffectAbility
    {
        public void_spirit_resonant_pulse(Ability ability)
            : base(ability)
        {
        }

        public override bool CanHit(params Unit[] targets)
        {
            return targets.All(x => x.Distance2D(this.Owner) < this.Radius);
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

        public float Radius
        {
            get
            {
                return Ability.GetAbilitySpecialData("radius");
            }
        }
    }
}