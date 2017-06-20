// <copyright file="item_power_treads.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.Items;

    public class item_power_treads : ActiveAbility
    {
        public item_power_treads(Item item)
            : base(item)
        {
        }

        public Attribute ActiveAttribute
        {
            get
            {
                return ((PowerTreads)this.Item).ActiveAttribute;
            }
        }

        // add power treads switch ?
    }
}