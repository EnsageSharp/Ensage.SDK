// <copyright file="faceless_void_time_walk.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_faceless_void
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class faceless_void_time_walk : RangedAbility, IHasModifier
    {
        public faceless_void_time_walk(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_faceless_void_time_walk";

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("speed");
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "range");
            }
        }
    }
}