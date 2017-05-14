// <copyright file="IInventoryManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Inventory
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using PlaySharp.Toolkit.Helper;

    using Inventory = Ensage.Inventory;

    public interface IInventoryManager : IControllable, INotifyCollectionChanged
    {
        Inventory Inventory { get; }

        HashSet<InventoryItem> Items { get; }

        Hero Owner { get; }
    }
}