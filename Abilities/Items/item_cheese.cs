// <copyright file="item_cheese.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_cheese : ActiveAbility, IHasHealthRestore, IHasManaRestore
    {
        public item_cheese(Item item)
            : base(item)
        {
        }

        public float TotalHealthRestore
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("health_restore");
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