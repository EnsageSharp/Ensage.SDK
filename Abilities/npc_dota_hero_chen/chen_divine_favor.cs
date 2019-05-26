// <copyright file="chen_test_of_faith.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_chen
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class chen_divine_favor : PassiveAbility, IHasHealthRestore, IAuraAbility
    {
        public chen_divine_favor(Ability ability)
            : base(ability)
        {
        }

        public float TotalHealthRestore
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("heal_rate");
            }
        }

        public string AuraModifierName { get; } = "modifier_chen_divine_favor";

        public float AuraRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("aura_radius");
            }
        }
    }
}