// <copyright file="enchantress_natures_attendants.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_enchantress
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class enchantress_natures_attendants : ActiveAbility, IHasModifier, IHasHealthRestore
    {
        public enchantress_natures_attendants(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_enchantress_natures_attendants";

        public float TotalHealthRestore
        {
            get
            {
                var heal = this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "heal");
                var wisps = this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "wisp_count");

                return heal * wisps * this.Duration;
            }
        }
    }
}