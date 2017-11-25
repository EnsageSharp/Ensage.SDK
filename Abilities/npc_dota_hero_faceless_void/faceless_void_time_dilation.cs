// <copyright file="faceless_void_time_dilation.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_faceless_void
{
    using Ensage.SDK.Extensions;

    public class faceless_void_time_dilation : ActiveAbility, IAreaOfEffectAbility
    {
        public faceless_void_time_dilation(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }
    }
}