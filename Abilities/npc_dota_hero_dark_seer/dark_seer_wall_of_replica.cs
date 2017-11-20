// <copyright file="dark_seer_wall_of_replica.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_dark_seer
{
    using Ensage.SDK.Extensions;

    public class dark_seer_wall_of_replica : RangedAbility, IAreaOfEffectAbility
    {
        public dark_seer_wall_of_replica(Ability ability)
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

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("width");
            }
        }
    }
}