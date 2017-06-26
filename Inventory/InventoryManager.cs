// <copyright file="InventoryManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Inventory
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Inventory.Metadata;
    using Ensage.SDK.Persistence;
    using Ensage.SDK.Service;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using Inventory = Ensage.Inventory;

    [ExportInventoryManager]
    public class InventoryManager : ControllableService, IInventoryManager
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private HashSet<InventoryItem> items;

        [ImportingConstructor]
        public InventoryManager([Import] IServiceContext context)
        {
            this.Owner = context.Owner;
            this.LastItems = new HashSet<InventoryItem>();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public Inventory Inventory => this.Owner.Inventory;

        public HashSet<InventoryItem> Items
        {
            get
            {
                if (this.items == null)
                {
                    this.items = this.Inventory.Items.Select(item => new InventoryItem(item)).ToHashSet();
                }

                return this.items;
            }
        }

        public Unit Owner { get; }

        private Dictionary<Type, List<PropertyBinding>> Bindings { get; } = new Dictionary<Type, List<PropertyBinding>>();

        private HashSet<InventoryItem> LastItems { get; set; }

        public void Attach(object target)
        {
            var targetType = target.GetType();

            foreach (var property in targetType.GetProperties())
            {
                var attribute = property.GetCustomAttribute<ItemBindingAttribute>();
                if (attribute != null)
                {
                    this.CreateBinding(property, target);
                }
            }
        }

        public void Detach(object target)
        {
            foreach (var binding in this.Bindings)
            {
                binding.Value.RemoveAll(e => !e.Reference.IsAlive || e.Reference.Target == target);
            }
        }

        public StockInfo GetStockInfo(AbilityId id, Team team = Team.Undefined)
        {
            if (team == Team.Undefined)
            {
                team = this.Owner.Team;
            }

            return Game.StockInfo.FirstOrDefault(e => e.AbilityId == id && e.Team == team);
        }

        protected override void OnActivate()
        {
            if (this.Owner.HasInventory)
            {
                UpdateManager.Subscribe(this.OnInventoryUpdate, 500);
                UpdateManager.SubscribeService(this.OnInventoryClear);
            }
        }

        protected override void OnDeactivate()
        {
            UpdateManager.Unsubscribe(this.OnInventoryUpdate);
            UpdateManager.Unsubscribe(this.OnInventoryClear);
        }

        private PropertyBinding CreateBinding(PropertyInfo prop, object target)
        {
            var itemType = prop.PropertyType;
            var binding = new PropertyBinding(prop, target);

            if (!this.Bindings.ContainsKey(itemType))
            {
                this.Bindings[itemType] = new List<PropertyBinding>();
            }

            Log.Debug($"Attach: {itemType.Name} @ {binding}");
            this.Bindings[itemType].Add(binding);

            return binding;
        }

        private void OnInventoryClear()
        {
            this.items = null;
        }

        private void OnInventoryUpdate()
        {
            if (this.Bindings.Count > 0 || this.CollectionChanged != null)
            {
                // inventory diff
                var added = this.Items.Except(this.LastItems).ToList();
                var removed = this.LastItems.Except(this.Items).ToList();

                // cleanup bindings
                foreach (var binding in this.Bindings)
                {
                    binding.Value.RemoveAll(e => !e.Reference.IsAlive);
                }

                // update bindings
                foreach (var change in added)
                {
                    var type = Type.GetType($"Ensage.SDK.Abilities.Items.{change.Id}");

                    if (type == null)
                    {
                        Log.Warn($"Item not supported {change.Id}");
                        continue;
                    }

                    if (this.Bindings.ContainsKey(type))
                    {
                        var item = Activator.CreateInstance(type, change.Item);

                        foreach (var binding in this.Bindings[type])
                        {
                            Log.Debug($"Update {binding}");
                            binding.SetValue(item);
                        }
                    }
                }

                foreach (var change in removed)
                {
                    var type = Type.GetType($"Ensage.SDK.Abilities.Items.{change.Id}");

                    if (type == null)
                    {
                        Log.Warn($"Item not supported {change.Id}");
                        continue;
                    }

                    if (this.Bindings.ContainsKey(type))
                    {
                        foreach (var binding in this.Bindings[type])
                        {
                            Log.Debug($"Update {binding}");
                            binding.SetValue(null);
                        }
                    }
                }

                // update events
                if (this.CollectionChanged != null)
                {
                    if (removed.Count > 0)
                    {
                        this.CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed));
                    }

                    if (added.Count > 0)
                    {
                        this.CollectionChanged.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, added));
                    }
                }

                this.LastItems = this.Items;
            }
        }
    }
}