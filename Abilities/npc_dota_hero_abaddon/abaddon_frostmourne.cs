// <copyright file="abaddon_frostmourne.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_abaddon
{
    public class abaddon_frostmourne : PassiveAbility, IHasModifier, IHasTargetModifier
    {
        public abaddon_frostmourne(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_abaddon_frostmourne_buff";

        public string TargetModifierName { get; } = "modifier_abaddon_frostmourne_debuff";
    }
}