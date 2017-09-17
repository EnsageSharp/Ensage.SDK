// <copyright file="legion_commander_press_the_attack.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_legion_commander
{
    public class legion_commander_press_the_attack : RangedAbility, IHasTargetModifier
    {
        public legion_commander_press_the_attack(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_legion_commander_press_the_attack";
    }
}