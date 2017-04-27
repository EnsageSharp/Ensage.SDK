// <copyright file="IInventoryManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Inventory
{
    using System.Collections.Immutable;
    using System.Collections.Specialized;

    using Inventory = Ensage.Inventory;

    public interface IInventoryManager : INotifyCollectionChanged
    {
        Inventory Inventory { get; }

        ImmutableHashSet<Item> Items { get; }

        Hero Owner { get; }
    }
}