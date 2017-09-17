// <copyright file="enchantress_enchant.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_enchantress
{
    public class enchantress_enchant : RangedAbility, IHasTargetModifier
    {
        public enchantress_enchant(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_enchantress_enchant_slow";
    }
}