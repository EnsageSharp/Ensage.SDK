// <copyright file="bristleback_bristleback.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bristleback
{
    using Ensage.SDK.Extensions;

    public class bristleback_bristleback : PassiveAbility
    {
        public bristleback_bristleback(Ability ability)
            : base(ability)
        {
        }

        public float BackDamageReduction
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("back_damage_reduction") / 100f;
            }
        }

        public float SideDamageReduction
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("side_damage_reduction") / 100f;
            }
        }
    }
}