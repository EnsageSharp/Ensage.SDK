// <copyright file="ItemBindingHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Inventory
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Ensage.SDK.Abilities;
    using Ensage.SDK.Persistence;

    

    using NLog;

    public class ItemBindingHandler
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private BaseAbility value;

        public ItemBindingHandler(AbilityId id, Type type)
        {
            this.Id = id;
            this.Type = type;
        }

        public AbilityId Id { get; }

        public Type Type { get; }

        public BaseAbility Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
                this.UpdateBindings(value);
            }
        }

        private List<PropertyBinding> Bindings { get; } = new List<PropertyBinding>();

        public PropertyBinding Add(PropertyInfo info, object target)
        {
            Log.Debug($"Attach [{this.Id}] {target}.{info.Name}");
            var binding = new PropertyBinding(info, target);

            this.Bindings.Add(binding);

            return binding;
        }

        public void Remove(object target)
        {
            this.Bindings.RemoveAll(e => !e.Reference.IsAlive || e.Reference.Target == target);
        }

        public void ResetBindings()
        {
            this.UpdateBindings(null);
        }

        public void UpdateBindings(object newValue)
        {
            foreach (var binding in this.Bindings)
            {
                try
                {
                    binding.SetValue(newValue);
                }
                catch (Exception e)
                {
                    Log.Warn(e);
                }
            }
        }
    }
}