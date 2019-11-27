// <copyright file="void_spirit_dissimilate.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_void_spirit
{
    using System;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Extensions;

    using SharpDX;

    public class void_spirit_dissimilate : ActiveAbility
    {
        public void_spirit_dissimilate(Ability ability)
            : base(ability)
        {
        }

        public float PhaseDuration
        {
            get
            {
                return Ability.GetAbilitySpecialData("phase_duration");
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