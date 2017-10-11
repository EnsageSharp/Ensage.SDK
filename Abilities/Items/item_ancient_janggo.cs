// <copyright file="item_ancient_janggo.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_ancient_janggo : ActiveAbility, IAreaOfEffectAbility, IHasModifier, IAuraAbility
    {
        public item_ancient_janggo(Item item)
            : base(item)
        {
        }

        public string AuraModifierName { get; } = "modifier_item_ancient_janggo_aura_effect";

        public float AuraRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public override bool CanBeCasted
        {
            get
            {
                return this.Item.CurrentCharges > 0 && base.CanBeCasted;
            }
        }

        public string ModifierName { get; } = "modifier_item_ancient_janggo_active";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }
    }
}