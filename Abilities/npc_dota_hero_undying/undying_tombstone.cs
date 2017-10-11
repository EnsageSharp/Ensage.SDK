// <copyright file="undying_tombstone.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_undying
{
    using Ensage.SDK.Abilities.Components;

    public class undying_tombstone : AreaOfEffectAbility, IHasTargetModifier
    {
        public undying_tombstone(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_undying_tombstone_zombie_deathstrike_slow_counter";
    }
}