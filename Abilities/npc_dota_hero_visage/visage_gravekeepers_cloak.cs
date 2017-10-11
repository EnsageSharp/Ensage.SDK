// <copyright file="visage_gravekeepers_cloak.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_visage
{
    using Ensage.SDK.Abilities.Components;

    public class visage_gravekeepers_cloak : PassiveAbility, IHasModifier
    {
        public visage_gravekeepers_cloak(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_visage_gravekeepers_cloak";
    }
}