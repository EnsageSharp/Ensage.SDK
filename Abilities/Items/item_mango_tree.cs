// <copyright file="item_royal_jelly.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    using PlaySharp.Toolkit.Helper.Annotations;

    [PublicAPI]
    public class item_mango_tree : RangedAbility
    {
        public item_mango_tree(Item item)
            : base(item)
        {
        }
    }
}