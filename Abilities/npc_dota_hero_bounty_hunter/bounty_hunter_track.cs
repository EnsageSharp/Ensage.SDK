// <copyright file="bounty_hunter_track.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bounty_hunter
{
    using Ensage.SDK.Abilities.Components;

    public class bounty_hunter_track : RangedAbility, IHasTargetModifier, IHasModifier
    {
        public bounty_hunter_track(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_bounty_hunter_track_effect";

        public string TargetModifierName { get; } = "modifier_bounty_hunter_track";
    }
}