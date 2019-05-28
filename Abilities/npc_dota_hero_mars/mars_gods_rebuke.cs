// <copyright file="mars_gods_rebuke.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_mars
{
    using Ensage.SDK.Extensions;

    public class mars_gods_rebuke : ConeAbility
    {
        public mars_gods_rebuke(Ability ability)
            : base(ability)
        {
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public float Angle
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("angle");
            }
        }
    }
}
