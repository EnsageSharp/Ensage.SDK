// <copyright file="slardar_sprint.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_slardar
{
    using Ensage.SDK.Abilities.Components;

    public class slardar_sprint : ActiveAbility, IHasModifier
    {
        public slardar_sprint(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_slardar_sprint";
    }
}