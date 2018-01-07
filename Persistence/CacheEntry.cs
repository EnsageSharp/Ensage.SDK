// <copyright file="CacheEntry.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    

    using NLog;

    public class CacheEntry
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly List<PropertyBinding> bindings = new List<PropertyBinding>();

        public CacheEntry(PersistenceCache cache, string key)
        {
            this.Cache = cache;
            this.Key = key;
        }

        public CacheEntry(PersistenceCache cache, string key, object value)
            : this(cache, key)
        {
            this.Value = value;
        }

        public string Key { get; }

        private PersistenceCache Cache { get; }

        private object Value { get; set; }

        public void Attach(PropertyInfo property, object target)
        {
            // cache -> target
            this.bindings.Add(new PropertyBinding(property, target));

            // target -> cache
            var changed = target as INotifyPropertyChanged;
            if (changed != null)
            {
                changed.PropertyChanged -= this.OnPropertyChanged;
                changed.PropertyChanged += this.OnPropertyChanged;
            }
        }

        public object GetValue()
        {
            return this.Value;
        }

        public void SetValue(object value, bool updateAttachments = true)
        {
            this.Value = value;

            if (updateAttachments)
            {
                this.UpdateAttachments();
            }
        }

        public void UpdateAttachments()
        {
            foreach (var binding in this.bindings.Where(b => b.Reference.IsAlive))
            {
                try
                {
                    var oldValue = binding.GetValue();

                    if (oldValue != null && !oldValue.Equals(this.Value))
                    {
                        binding.SetValue(this.Value);
                    }
                }
                catch (Exception e)
                {
                    Log.Warn($"Binding-Update failed with {e.Message}");
                }
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            try
            {
                var binding = this.bindings.FirstOrDefault(b => b.PropertyInfo.Name == args.PropertyName && b.Reference.Target == sender);

                if (binding != null)
                {
                    this.Cache.SetValue(this.Key, binding.GetValue(), false);
                }
            }
            catch (Exception e)
            {
                Log.Warn($"Binding-Update failed with {e.Message}");
            }
        }
    }
}