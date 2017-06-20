// <copyright file="item_ward_observer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using System.Linq;

    public class item_ward_observer : RangedAbility, IItemInfo
    {
        public item_ward_observer(Item item)
            : base(item)
        {
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