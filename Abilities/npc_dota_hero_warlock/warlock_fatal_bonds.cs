// <copyright file="warlock_fatal_bonds.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_warlock
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class warlock_fatal_bonds : CircleAbility, IHasTargetModifier
    {
        public warlock_fatal_bonds(Ability ability)
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

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("search_aoe");
            }
        }

        public string TargetModifierName { get; } = "modifier_warlock_fatal_bonds";
    }
}