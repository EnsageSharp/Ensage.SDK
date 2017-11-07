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

        public new float CastRange(bool isEnemy = false)
        {
            if (!isEnemy)
            {
                return base.CastRange;
            }

            var castRangeEnemy = this.Ability.GetAbilitySpecialData("cast_range_enemy");

            var aetherLens = this.Owner.GetItemById(AbilityId.item_aether_lens);
            if (aetherLens != null)
            {
                castRangeEnemy += aetherLens.GetAbilitySpecialData("cast_range_bonus");
            }

            return castRangeEnemy;
        }

        public override bool CanHit(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return true;
            }

            if (this.Owner.Distance2D(targets.First()) < this.CastRange(targets.First().IsEnemy(this.Owner)))
            {
                return true;
            }

            return false;
        }
    }
}