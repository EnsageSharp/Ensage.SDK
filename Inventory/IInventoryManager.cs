// <copyright file="IInventoryManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Inventory
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    using Inventory = Ensage.Inventory;

    public interface IInventoryManager : INotifyCollectionChanged
    {
        Inventory Inventory { get; }

        HashSet<InventoryItem> Items { get; }

        Hero Owner { get; }
    }
}