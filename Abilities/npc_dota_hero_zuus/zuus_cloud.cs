// <copyright file="zuus_cloud.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_zuus
{
    using Ensage.SDK.Extensions;

    public class zuus_cloud : RangedAbility, IAreaOfEffectAbility
    {
        public zuus_cloud(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("cloud_radius");
            }
        }
    }
}