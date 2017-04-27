// <copyright file="InventoryManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Inventory
{
    using System.Collections.Immutable;
    using System.Collections.Specialized;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Inventory.Metadata;
    using Ensage.SDK.Service;

    using Inventory = Ensage.Inventory;

    [ExportInventoryManager]
    public class InventoryManager : IInventoryManager
    {
        private ImmutableHashSet<Item> items;

        [ImportingConstructor]
        public InventoryManager(IServiceContext context)
        {
            this.LastItems = ImmutableHashSet<Item>.Empty;
            this.Owner = context.Owner;

            UpdateManager.Subscribe(this.OnInventoryUpdate, 500);
            UpdateManager.SubscribeService(this.OnInventoryClear);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public Inventory Inventory => this.Owner.Inventory;

        public ImmutableHashSet<Item> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = this.Inventory.Items.ToImmutableHashSet();
                }

                return this.items;
            }
        }

        public Hero Owner { get; }

        private ImmutableHashSet<Item> LastItems { get; set; }

        private void OnInventoryClear()
        {
            this.items = null;
        }

        private void OnInventoryUpdate()
        {
            if (this.CollectionChanged != null)
            {
                var added = this.LastItems.Except(this.Items);
                this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, added));

                var removed = this.Items.Except(this.LastItems);
                this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed));
            }

            this.LastItems = this.Items;
        }
    }
}