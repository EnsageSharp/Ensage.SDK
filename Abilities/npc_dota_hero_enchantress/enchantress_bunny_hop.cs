// <copyright file="enchantress_bunny_hop.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Extensions;

namespace Ensage.SDK.Abilities.npc_dota_hero_enchantress
{
    public class enchantress_bunny_hop : ActiveAbility
    {
        public enchantress_bunny_hop(Ability ability)
            : base(ability)
        {
        }

        public float HopDistance
        {
            get
            {
                return Ability.GetAbilitySpecialData("hop_distance");
            }
        }

        public override float Duration
        {
            get
            {
                return Ability.GetAbilitySpecialData("hop_duration");
            }
        }
    }
}
