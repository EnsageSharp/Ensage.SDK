// <copyright file="zuus_arc_lightning.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_zuus
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class zuus_arc_lightning : RangedAbility, IAreaOfEffectAbility
    {
        private readonly zuus_static_field staticField;

        public zuus_arc_lightning(Ability ability)
            : base(ability)
        {
            var passive = this.Owner.GetAbilityById(AbilityId.zuus_static_field);
            if (passive != null)
            {
                this.staticField = new zuus_static_field(passive);
            }
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        protected override float RawDamage
        {
            get
            {
                var damage = base.RawDamage;

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_zeus_2);
                if (talent != null && talent.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
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

            return totalDamage + (this.staticField?.GetDamage(targets) ?? 0);
        }
    }
}