// <copyright file="sven_great_cleave.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_sven
{
    using Ensage.SDK.Extensions;

    public class sven_great_cleave : PassiveAbility, IAreaOfEffectAbility
    {
        public sven_great_cleave(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("cleave_distance");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("great_cleave_damage");
            }
        }
    }
}