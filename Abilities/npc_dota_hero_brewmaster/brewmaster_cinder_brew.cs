// <copyright file="brewmaster_drunken_haze.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_brewmaster
{
    using Ensage.SDK.Abilities.Components;

    public class brewmaster_cinder_brew : CircleAbility, IHasTargetModifier
    {
        public brewmaster_cinder_brew(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_brewmaster_cinder_brew";
    }
}