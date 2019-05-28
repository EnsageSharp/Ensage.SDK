// <copyright file="mars_arena_of_blood.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_mars
{
    using Ensage.SDK.Extensions;

    public class mars_arena_of_blood : CircleAbility, IAreaOfEffectAbility
    {
        public mars_arena_of_blood(Ability ability) 
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
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }
    }
}
