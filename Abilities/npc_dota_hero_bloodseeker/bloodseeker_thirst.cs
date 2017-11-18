// <copyright file="bloodseeker_thirst.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bloodseeker
{
    using Ensage.SDK.Abilities.Components;

    public class bloodseeker_thirst : PassiveAbility, IHasTargetModifier, IHasModifier
    {
        public bloodseeker_thirst(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_bloodseeker_thirst_speed";

        public string TargetModifierName { get; } = "modifier_bloodseeker_thirst_vision";
    }
}