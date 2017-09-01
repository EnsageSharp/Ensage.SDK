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
                return this.Ability.GetAbilitySpecialData("cleave_damage_pct");
            }
        }

        public string TargetModifierName { get; } = "modifier_magnataur_empower";
    }
}