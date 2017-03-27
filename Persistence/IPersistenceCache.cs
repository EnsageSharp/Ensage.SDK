// <copyright file="IPersistenceCache.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Persistence
{
    using System;

    public interface IPersistenceCache
    {
        void Clear();

        void CreateOrUpdate(string key, object value);

        T GetOrCreate<T>(string key, Func<T> factory = null);

        void Save();
    }
}