// <copyright file="item_gem.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_gem : PassiveAbility, IItemInfo, IHasModifier, IAreaOfEffectAbility
    {
        public item_gem(Item item)
            : base(item)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.ProvidesVision;

        public string ModifierName { get; } = "modifier_item_gem_of_true_sight";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public int StockCount
        {
            get
            {
                var itemStockInfo = Game.StockInfo.FirstOrDefault(x => x.AbilityId == this.Ability.Id && x.Team == this.Owner.Team);
                return itemStockInfo?.StockCount ?? 0;
            }
        }
    }
}