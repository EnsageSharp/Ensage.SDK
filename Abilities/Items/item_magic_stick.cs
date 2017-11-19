// <copyright file="item_magic_stick.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_magic_stick : ActiveAbility, IHasHealthRestore, IHasManaRestore
    {
        public item_magic_stick(Item item)
            : base(item)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return this.Item.CurrentCharges > 0 && base.CanBeCasted;
            }
        }

        public float TotalHealthRestore
        {
            get
            {
                var chargeRestore = this.Ability.GetAbilitySpecialData("restore_per_charge");
                return this.Item.CurrentCharges * chargeRestore;
            }
        }

        public float TotalManaRestore
        {
            get
            {
                return this.TotalHealthRestore;
            }
        }
    }
}