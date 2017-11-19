// <copyright file="chen_test_of_faith.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_chen
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class chen_test_of_faith : RangedAbility, IHasHealthRestore
    {
        public chen_test_of_faith(Ability ability)
            : base(ability)
        {
        }

        public float TotalHealthRestore
        {
            get
            {
                // average
                return (this.Ability.GetAbilitySpecialData("heal_min") + this.Ability.GetAbilitySpecialData("heal_max")) / 2;
            }
        }

        protected override float RawDamage
        {
            get
            {
                // lets take average damage
                return (this.Ability.GetAbilitySpecialData("damage_min") + this.Ability.GetAbilitySpecialData("damage_max")) / 2;
            }
        }
    }
}