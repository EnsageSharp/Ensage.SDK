// <copyright file="PersistenceCache.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using log4net;

    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    public class PersistenceCache : IPersistenceCache
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool disposed;

        public PersistenceCache()
        {
            this.StoreFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "Assemblies", "ensage.sdk.json");

            this.Load();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event PropertyChangingEventHandler PropertyChanging;

        public IEnumerable<CacheEntry> Entries => this.Cache.Values;

        private Dictionary<string, CacheEntry> Cache { get; set; } = new Dictionary<string, CacheEntry>();

        private string StoreFile { get; }

        public CacheEntry this[string key]
        {
            get
            {
                if (!this.Cache.ContainsKey(key))
                {
                    return new CacheEntry(this, key, null);
                }

                return this.Cache[key];
            }
        }

        public T Attach<T>(T target = null)
            where T : class, new()
        {
            return (T)this.Attach(typeof(T).Name, target ?? new T());
        }

        public object Attach(string ns, object target)
        {
            var targetType = target.GetType();

            foreach (var property in targetType.GetProperties())
            {
                var attribute = property.GetCustomAttribute<PropertyBindingAttribute>();
                if (attribute != null)
                {
                    CacheEntry entry;
                    var key = attribute.Name ?? $"{ns}.{property.Name}";
                    var defaultValue = property.GetValue(target);

                    if (this.Cache.TryGetValue(key, out entry))
                    {
                        Log.Debug($"Update-Attachment: {key} @ {target}.{property.Name}");

                        entry = this.Cache[key];
                        entry.Attach(property, target);
                        entry.UpdateAttachments();
                    }
                    else
                    {
                        Log.Debug($"Create-Attachment: {key} @ {target}.{property.Name}");

                        entry = new CacheEntry(this, key, defaultValue);
                        entry.Attach(property, target);

                        this.Cache.Add(key, entry);
                    }
                }
                else
                {
                    this.Attach($"{ns}.{property.Name}", property.GetValue(target));
                }
            }

            return target;
        }

        public void Clear()
        {
            this.Cache.Clear();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public CacheEntry GetOrCreate(string key, Func<object> valueFactory)
        {
            CacheEntry entry = null;

            if (!this.Cache.TryGetValue(key, out entry))
            {
                entry = new CacheEntry(this, key, valueFactory());

                this.Cache.Add(key, entry);
            }

            return entry;
        }

        public T GetValue<T>(string key, Func<object> valueFactory = null)
        {
            return (T)this.Cache[key].GetValue();
        }

        public void Load()
        {
            try
            {
                this.Cache = new Dictionary<string, CacheEntry>();

                if (File.Exists(this.StoreFile))
                {
                    var data = JsonFactory.FromFile<Dictionary<string, ObjectValue>>(this.StoreFile);

                    foreach (var entry in data)
                    {
                        this.Cache[entry.Key] = new CacheEntry(this, entry.Key, Convert.ChangeType(entry.Value.Value, entry.Value.Type));
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public void Save()
        {
            try
            {
                var data = this.Cache.Where(e => e.Value.GetValue() != null).ToDictionary(e => e.Key, e => new ObjectValue(e.Value.GetValue()));

                JsonFactory.ToFile(this.StoreFile, data);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public void SetValue<T>(string key, T value, bool @override = false)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var entry = this.GetOrCreate(key, () => value);

            if (!@override && entry.GetValue() != null && EqualityComparer<T>.Default.Equals((T)entry.GetValue(), value))
            {
                return;
            }

            this.OnPropertyChanging(key);
            entry.SetValue(value, true);
            this.OnPropertyChanged(key);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.Save();
            }

            this.disposed = true;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }
    }
}