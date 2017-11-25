// <copyright file="antimage_mana_void.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_antimage
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class antimage_mana_void : AreaOfEffectAbility
    {
        public antimage_mana_void(Ability ability)
            : base(ability)
        {
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("mana_void_aoe_radius");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("mana_void_damage_per_mana");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return 0;
            }

            var mainTarget = targets.First();
            if (mainTarget.MaximumMana <= 0)
            {
                return 0;
            }

            var amplify = this.Ability.SpellAmplification();
            var manaDamage = (mainTarget.MaximumMana - mainTarget.Mana) * this.RawDamage;

            var totalDamage = 0f;

            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(manaDamage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}