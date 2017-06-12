﻿// <copyright file="drow_ranger_frost_arrows.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_drow_ranger
{
    using Ensage.SDK.Extensions;

    public class drow_ranger_frost_arrows : OrbAbility, IHasTargetModifier
    {
        public drow_ranger_frost_arrows(Ability ability)
            : base(ability)
        {
        }

        public override float Speed
        {
            get
            {
                return this.Owner.ProjectileSpeed();
            }
        }

        public string TargetModifierName { get; } = "modifier_drow_ranger_frost_arrows_slow";
    }
}