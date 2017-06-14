// <copyright file="rattletrap_battery_assault.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_rattletrap
{
    using Ensage.SDK.Extensions;

    public class rattletrap_battery_assault : ActiveAbility, IAreaOfEffectAbility
    {
        public rattletrap_battery_assault(Ability ability)
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