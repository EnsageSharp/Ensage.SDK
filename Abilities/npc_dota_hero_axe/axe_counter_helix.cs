// <copyright file="axe_counter_helix.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_axe
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class axe_counter_helix : PassiveAbility, IAreaOfEffectAbility
    {
        public axe_counter_helix(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}