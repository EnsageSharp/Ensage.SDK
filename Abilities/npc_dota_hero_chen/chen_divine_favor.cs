// <copyright file="chen_test_of_faith.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_chen
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class chen_divine_favor : RangedAbility, IHasHealthRestore, IHasModifier
    {
        public chen_divine_favor(Ability ability)
            : base(ability)
        {
        }

        public float TotalHealthRestore
        {
            get
            {
                // average
                return this.Ability.GetAbilitySpecialData("heal_rate") * this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public string ModifierName { get; } = "modifier_chen_divine_favor";
    }
}