// <copyright file="item_guardian_greaves.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_guardian_greaves : ActiveAbility, IAreaOfEffectAbility, IHasModifier, IAuraAbility, IHasHealthRestore, IHasManaRestore
    {
        public item_guardian_greaves(Item item)
            : base(item)
        {
        }

        public string AuraModifierName { get; } = "modifier_item_guardian_greaves_aura";

        public float AuraRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("aura_radius");
            }
        }

        public string ModifierName { get; } = "modifier_item_mekansm_noheal";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("replenish_radius");
            }
        }

        public float TotalHealthRestore
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("replenish_health");
            }
        }

        public float TotalManaRestore
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("replenish_mana");
            }
        }
    }
}