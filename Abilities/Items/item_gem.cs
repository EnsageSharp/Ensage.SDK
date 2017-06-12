namespace Ensage.SDK.Abilities.Items
{
    using System.Linq;

    public class item_gem : PassiveAbility, IItemInfo, IHasModifier
    {
        public item_gem(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_item_gem_of_true_sight";

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