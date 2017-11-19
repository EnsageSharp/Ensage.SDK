// <copyright file="legion_commander_duel.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_legion_commander
{
    using Ensage.SDK.Abilities.Components;

    public class legion_commander_duel : RangedAbility, IHasModifier, IHasTargetModifier
    {
        public legion_commander_duel(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public string ModifierName { get; } = "modifier_legion_commander_duel";

        public string TargetModifierName { get; } = "modifier_legion_commander_duel";
    }
}