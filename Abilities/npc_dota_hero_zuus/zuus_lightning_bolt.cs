// <copyright file="zuus_lightning_bolt.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_zuus
{
    using Ensage.SDK.Extensions;

    public class zuus_lightning_bolt : RangedAbility, IAreaOfEffectAbility
    {
        public zuus_lightning_bolt(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("spread_aoe");
            }
        }
    }
}