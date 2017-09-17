// <copyright file="enchantress_untouchable.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_enchantress
{
    public class enchantress_untouchable : PassiveAbility, IHasTargetModifier
    {
        public enchantress_untouchable(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_enchantress_untouchable_slow";
    }
}