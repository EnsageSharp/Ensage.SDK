// <copyright file="InventoryManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Inventory
{
    using System.Collections.Immutable;
    using System.Collections.Specialized;
    using System.ComponentModel.Composition;
    using System.Reflection;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Inventory.Metadata;
    using Ensage.SDK.Service;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using Inventory = Ensage.Inventory;

    [ExportInventoryManager]
    public class InventoryManager : IInventoryManager
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
                var changes = this.Items.SymmetricExcept(this.LastItems);

                foreach (var change in changes)
                {
                    if (!this.LastItems.Contains(change))
                    {
                        Log.Debug($"Added {change.Id}");
                        this.CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, change));
                    }
                }

                this.LastItems = this.Items;
            }
        }
    }
}