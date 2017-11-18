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

        public override float EndRadius
        {
            get
            {
                return 206.17f; // no in special data
            }
        }

        public override bool IsReady
        {
            get
            {
                return this.Ability.IsActivated && base.IsReady;
            }
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("axe_width");
            }
        }

        public override float Range
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("axe_range");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("axe_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_troll_warlord_whirling_axes_slow";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "axe_damage");
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