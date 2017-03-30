// <copyright file="IPersistenceCache.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface IPersistenceCache
    {
        event PropertyChangedEventHandler PropertyChanged;

        event PropertyChangingEventHandler PropertyChanging;

        IEnumerable<CacheEntry> Entries { get; }

        CacheEntry this[string key] { get; }

        void Clear();

        void Dispose();

        T GetValue<T>(string key, Func<object> valueFactory = null);

        void Load();

        void Save();

        void SetValue<T>(string key, T value, bool @override = true);
    }
}