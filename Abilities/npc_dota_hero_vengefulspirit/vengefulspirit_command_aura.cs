// <copyright file="vengefulspirit_command_aura.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_vengefulspirit
{
    using Ensage.SDK.Abilities.Components;

    public class vengefulspirit_command_aura : AuraAbility, IHasTargetModifier
    {
        public vengefulspirit_command_aura(Ability ability)
            : base(ability)
        {
        }

        public override string AuraModifierName { get; } = "modifier_vengefulspirit_command_aura_effect";

        public string TargetModifierName { get; } = "modifier_vengefulspirit_command_negative_aura_effect";
    }
}