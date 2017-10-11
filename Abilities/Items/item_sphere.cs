// <copyright file="item_sphere.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_sphere : RangedAbility, IHasTargetModifier
    {
        public item_sphere(Item item)
            : base(item)
        {
        }

        public string TargetModifierName { get; } = "modifier_item_sphere_target";
    }
}