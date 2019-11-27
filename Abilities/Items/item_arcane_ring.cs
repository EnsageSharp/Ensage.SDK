// <copyright file="item_arcane_ring.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_arcane_ring : ActiveAbility, IAreaOfEffectAbility, IHasManaRestore
    {
        public item_arcane_ring(Item item)
            : base(item)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public float TotalManaRestore
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("mana_restore");
            }
        }
    }
}