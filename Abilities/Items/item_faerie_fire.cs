// <copyright file="item_faerie_fire.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_faerie_fire : ActiveAbility, IHasHealthRestore
    {
        public item_faerie_fire(Item item)
            : base(item)
        {
        }

        public float TotalHealthRestore
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("hp_restore");
            }
        }
    }
}