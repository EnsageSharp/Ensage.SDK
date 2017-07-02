// <copyright file="undying_tombstone.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_undying
{
    public class undying_tombstone : RangedAbility, IHasTargetModifier
    {
        public undying_tombstone(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_undying_tombstone_zombie_deathstrike_slow_counter";
    }
}