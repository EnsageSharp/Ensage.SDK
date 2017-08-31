// <copyright file="visage_grave_chill.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_visage
{
    public class visage_grave_chill : RangedAbility, IHasTargetModifier, IHasModifier
    {
        public visage_grave_chill(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_visage_grave_chill_buff";

        public string TargetModifierName { get; } = "modifier_visage_grave_chill_debuff";
    }
}