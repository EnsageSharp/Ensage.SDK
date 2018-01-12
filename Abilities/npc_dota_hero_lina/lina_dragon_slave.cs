// <copyright file="lina_dragon_slave.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_lina
{
    using Ensage.SDK.Extensions;

    public class lina_dragon_slave : ConeAbility
    {
        public lina_dragon_slave(Ability ability)
            : base(ability)
        {
        }

        public override float EndRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("dragon_slave_width_end");
            }
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("dragon_slave_width_initial");
            }
        }

        public override float Range
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("dragon_slave_distance");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("dragon_slave_speed");
            }
        }
    }
}