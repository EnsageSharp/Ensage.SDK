// <copyright file="faceless_void_chronosphere.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_faceless_void
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class faceless_void_chronosphere : CircleAbility, IHasTargetModifier, IHasModifier
    {
        public faceless_void_chronosphere(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public string ModifierName { get; } = "modifier_faceless_void_chronosphere_speed";

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_faceless_void_chronosphere_freeze";
    }
}