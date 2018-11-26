// <copyright file="bounty_hunter_wind_walk.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bounty_hunter
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class bounty_hunter_wind_walk : ActiveAbility, IHasModifier, IHasTargetModifier
    {
        public bounty_hunter_wind_walk(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Invisible;

        public string ModifierName { get; } = "modifier_bounty_hunter_wind_walk";
        public string TargetModifierName { get; } = "modifier_bounty_hunter_wind_walk_slow";
    }
}