// <copyright file="huskar_inner_fire.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_huskar
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class huskar_inner_fire : ActiveAbility, IHasTargetModifier, IAreaOfEffectAbility
    {
        public huskar_inner_fire(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Disarmed;

        public string TargetModifierName { get; } = "modifier_huskar_inner_fire_knockback";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }
    }
}