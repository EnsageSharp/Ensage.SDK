// <copyright file="item_clarity.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_clarity : RangedAbility, IHasTargetModifier, IHasManaRestore
    {
        public item_clarity(Item item)
            : base(item)
        {
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("buff_duration");
            }
        }

        public string TargetModifierName { get; } = "modifier_clarity_potion";

        public float TotalManaRestore
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("mana_regen") * this.Duration;
            }
        }
    }
}