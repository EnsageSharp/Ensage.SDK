// <copyright file="item_mjollnir.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_mjollnir : RangedAbility, IHasTargetModifier, IAreaOfEffectAbility
    {
        public item_mjollnir(Item item)
            : base(item)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("static_radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_item_mjollnir_static";
    }
}