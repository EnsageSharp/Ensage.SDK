// <copyright file="night_stalker_void.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_night_stalker
{
    using Ensage.SDK.Abilities.Components;

    public class night_stalker_void : RangedAbility, IHasTargetModifier
    {
        public night_stalker_void(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public string TargetModifierName { get; } = "modifier_night_stalker_void";
    }
}