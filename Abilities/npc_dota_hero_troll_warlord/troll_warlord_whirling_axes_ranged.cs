// <copyright file="troll_warlord_whirling_axes_ranged.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_troll_warlord
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class troll_warlord_whirling_axes_ranged : ConeAbility, IHasTargetModifier
    {
        public troll_warlord_whirling_axes_ranged(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_troll_warlord_whirling_axes_slow";

        public override float Range
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("axe_range");
            }
        }

        public override float EndRadius
        {
            get
            {
                return 206.17f; // no in special data
            }
        }

        protected override string RadiusName { get; } = "axe_width";

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("axe_damage");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_troll_warlord_3);
                if (talent?.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
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