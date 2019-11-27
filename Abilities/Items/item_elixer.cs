// <copyright file="item_elixer.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.Items;
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_elixer : RangedAbility, IHasTargetModifier, IHasHealthRestore, IHasManaRestore
    {
        public item_elixer(Item item)
            : base(item)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return this.Item?.CurrentCharges > 0 && base.CanBeCasted;
            }
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public string TargetModifierName { get; } = "modifier_elixer_healing";

        public float TotalHealthRestore
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("health") * this.Duration;
            }
        }

        public float TotalManaRestore
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("mana") * this.Duration;
            }
        }
    }
}