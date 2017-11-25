// <copyright file="magnataur_skewer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_magnataur
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class magnataur_skewer : LineAbility, IHasTargetModifier, IHasModifier
    {
        public magnataur_skewer(Ability ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return base.CanBeCasted && !this.Owner.IsRooted();
            }
        }

        public string ModifierName { get; } = "modifier_magnataur_skewer_movement";

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("skewer_radius");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("skewer_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_magnataur_skewer_impact";

        protected override float BaseCastRange
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "range");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("skewer_damage");
            }
        }
    }
}