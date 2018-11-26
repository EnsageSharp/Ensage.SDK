// <copyright file="antimage_spell_shield.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Abilities.Components;

namespace Ensage.SDK.Abilities.npc_dota_hero_antimage
{
    public class antimage_counterspell : ActiveAbility, IHasModifier
    {
        public antimage_counterspell(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_antimage_counterspell";
    }
}