﻿// <copyright file="arc_warden_tempest_double.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_arc_warden
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class arc_warden_tempest_double : ActiveAbility, IHasTargetModifier
    {
        public arc_warden_tempest_double(Ability ability)
            : base(ability)
        {
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public string TargetModifierName { get; } = "modifier_arc_warden_tempest_double";
    }
}