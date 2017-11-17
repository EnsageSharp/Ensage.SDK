// <copyright file="batrider_sticky_napalm.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_batrider
{
    using Ensage.SDK.Extensions;

    public class batrider_sticky_napalm : CircleAbility
    {
        public batrider_sticky_napalm(Ability ability)
            : base(ability)
        {
        }

        public float BonusDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }
    }
}