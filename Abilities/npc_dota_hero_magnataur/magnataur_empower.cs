// <copyright file="magnataur_empower.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_magnataur
{
    using Ensage.SDK.Extensions;

    public class magnataur_empower : RangedAbility, IHasTargetModifier, IAreaOfEffectAbility
    {
        public magnataur_empower(Ability ability)
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

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("cleave_damage_pct");
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_magnus_2);
                if (talent != null && talent.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }

        public string TargetModifierName { get; } = "modifier_magnataur_empower";
    }
}