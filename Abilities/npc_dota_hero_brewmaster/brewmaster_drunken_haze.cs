// <copyright file="brewmaster_drunken_haze.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_brewmaster
{
    using Ensage.SDK.Abilities.Components;

    public class brewmaster_drunken_haze : RangedAbility, IHasTargetModifier
    {
        public brewmaster_drunken_haze(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_brewmaster_drunken_haze";
    }
}