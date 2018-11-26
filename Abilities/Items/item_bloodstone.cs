// <copyright file="item_bloodstone.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Abilities.Components;

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_bloodstone : ActiveAbility, IHasModifier, IHasHealthRestore
    {
        public item_bloodstone(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_item_bloodstone_active";

        public float TotalHealthRestore
        {
            get
            {
                return ((float)this.Owner.Mana * this.Ability.GetAbilitySpecialData("mana_cost_percentage")) / 100f;
            }
        }
    }
}