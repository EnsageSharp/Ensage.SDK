// <copyright file="item_bottle.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.Items;
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_bottle : RangedAbility, IHasTargetModifier, IHasHealthRestore, IHasManaRestore
    {
        public item_bottle(Item item)
            : base(item)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return this.Item.CurrentCharges > 0 && this.StoredRune == RuneType.None && base.CanBeCasted;
            }
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("restore_time");
            }
        }

        public RuneType StoredRune
        {
            get
            {
                return ((Bottle)this.Item).StoredRune;
            }
        }

        public string TargetModifierName { get; } = "modifier_bottle_regeneration";

        public float TotalHealthRestore
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("health_restore") * this.Duration;
            }
        }

        public float TotalManaRestore
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("mana_restore") * this.Duration;
            }
        }
    }
}