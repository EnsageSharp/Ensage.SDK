// <copyright file="death_prophet_carrion_swarm.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_death_prophet
{
    using Ensage.SDK.Extensions;

    public class death_prophet_carrion_swarm : ConeAbility
    {
        public death_prophet_carrion_swarm(Ability ability)
            : base(ability)
        {
        }

        public override float EndRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("end_radius");
            }
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("start_radius");
            }
        }

        public override float Range
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("range");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("speed");
            }
        }
    }
}