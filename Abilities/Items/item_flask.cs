// <copyright file="item_flask.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_flask : RangedAbility, IHasTargetModifier, IHasHealthRestore
    {
        public item_flask(Item item)
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

        public string TargetModifierName { get; } = "modifier_flask_healing";

        public float TotalHealthRestore
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("health_regen") * this.Duration;
            }
        }
    }
}