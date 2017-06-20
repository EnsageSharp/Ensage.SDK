// <copyright file="item_arcane_boots.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_arcane_boots : ActiveAbility, IAreaOfEffectAbility
    {
        public item_arcane_boots(Item item)
            : base(item)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("replenish_radius");
            }
        }
    }
}