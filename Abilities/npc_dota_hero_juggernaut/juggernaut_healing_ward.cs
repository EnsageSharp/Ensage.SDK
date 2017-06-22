// <copyright file="juggernaut_healing_ward.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_juggernaut
{
    using Ensage.SDK.Extensions;

    public class juggernaut_healing_ward : RangedAbility, IAreaOfEffectAbility
    {
        public juggernaut_healing_ward(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("healing_ward_aura_radius");
            }
        }
    }
}