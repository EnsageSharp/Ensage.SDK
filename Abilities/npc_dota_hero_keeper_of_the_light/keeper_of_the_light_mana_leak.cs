// <copyright file="keeper_of_the_light_mana_leak.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_keeper_of_the_light
{
    using Ensage.SDK.Abilities.Components;

    public class keeper_of_the_light_mana_leak : RangedAbility, IHasTargetModifier
    {
        public keeper_of_the_light_mana_leak(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_keeper_of_the_light_mana_leak";
    }
}