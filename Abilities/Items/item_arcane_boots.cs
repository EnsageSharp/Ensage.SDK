// <copyright file="item_arcane_boots.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Abilities.Components;

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_arcane_boots : ActiveAbility, IAreaOfEffectAbility, IHasManaRestore
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

        public float TotalManaRestore
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("replenish_amount");
            }
        }
    }
}