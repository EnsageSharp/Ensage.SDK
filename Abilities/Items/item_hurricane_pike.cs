// <copyright file="item_hurricane_pike.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_hurricane_pike : RangedAbility, IHasTargetModifier, IHasModifier
    {
        public item_hurricane_pike(Item item)
            : base(item)
        {
        }

        public float EnemyCastRange
        {
            get
            {
                var bonusRange = this.CastRange - this.BaseCastRange;
                return this.Ability.GetAbilitySpecialData("cast_range_enemy") + bonusRange;
            }
        }

        public string ModifierName { get; } = "modifier_item_hurricane_pike_range";

        public float PushLength
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("push_length");
            }
        }

        public float PushSpeed { get; } = 1500.0f;

        public string TargetModifierName { get; } = "modifier_item_hurricane_pike_active";

        public override bool CanHit(params Unit[] targets)
        {
            var target = targets.FirstOrDefault();
            if (target == null)
            {
                return true;
            }

            var castRange = target.IsAlly(this.Owner) ? this.CastRange : this.EnemyCastRange;
            if (this.Owner.Distance2D(target) < castRange)
            {
                return true;
            }

            return false;
        }
    }
}