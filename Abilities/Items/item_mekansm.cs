// <copyright file="item_mekansm.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_mekansm : ActiveAbility, IAreaOfEffectAbility, IAuraAbility, IHasModifier
    {
        public item_mekansm(Item item)
            : base(item)
        {
        }

        public string AuraModifierName { get; } = "modifier_item_mekansm_aura";

        public float AuraRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("aura_radius");
            }
        }

        public string ModifierName { get; } = "modifier_item_mekansm_spell";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("heal_radius");
            }
        }
    }
}