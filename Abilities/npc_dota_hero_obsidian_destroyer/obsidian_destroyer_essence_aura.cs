// <copyright file="obsidian_destroyer_essence_aura.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_obsidian_destroyer
{
    public class obsidian_destroyer_essence_aura : AuraAbility
    {
        public obsidian_destroyer_essence_aura(Ability ability)
            : base(ability)
        {
        }

        public override string AuraModifierName { get; } = "modifier_obsidian_destroyer_essence_aura_effect";
    }
}