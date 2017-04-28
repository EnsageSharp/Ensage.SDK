// <copyright file="InventoryManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Inventory
{
    using System;
    using System.Collections.Immutable;
    using System.Collections.Specialized;
    using System.ComponentModel.Composition;
    using System.Linq;
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

        private ImmutableHashSet<InventoryItem> items;

        [ImportingConstructor]
        public InventoryManager([Import] IServiceContext context)
        {
            this.Owner = context.Owner;
            this.LastItems = ImmutableHashSet<InventoryItem>.Empty;

            UpdateManager.Subscribe(this.OnInventoryUpdate, 500);
            UpdateManager.SubscribeService(this.OnInventoryClear);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public Inventory Inventory => this.Owner.Inventory;

        public ImmutableHashSet<InventoryItem> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = this.Inventory.Items.Select(item => new InventoryItem(item)).ToImmutableHashSet();
                }

                return this.items;
            }
        }

        public Hero Owner { get; }

        private ImmutableHashSet<InventoryItem> LastItems { get; set; }

        private void OnInventoryClear()
        {
            this.items = null;
        }

        private void OnInventoryUpdate()
        {
            if (this.CollectionChanged != null)
            {
                var added = this.Items.Except(this.LastItems);
                var removed = this.LastItems.Except(this.Items);

                foreach (var change in added)
                {
                    try
                    {
                        Log.Debug($"Added {change.Id}");
                        this.CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, change));
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }

                foreach (var change in removed)
                {
                    try
                    {
                        Log.Debug($"Removed {change.Id}");
                        this.CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, change));
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }

                this.LastItems = this.Items;
            }
        }
    }
}