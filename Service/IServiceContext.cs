// <copyright file="IServiceContext.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Collections.Generic;

    using PlaySharp.Toolkit.Helper.Annotations;

    public interface IServiceContext : IDisposable
    {
        ContextContainer<IServiceContext> Container { get; }

        Unit Owner { get; }

        T Get<T>([CanBeNull] string contract = null);

        IEnumerable<T> GetAll<T>([CanBeNull] string contract = null);

        Lazy<T> GetExport<T>([CanBeNull] string contract = null);

        IEnumerable<Lazy<T, TMetadata>> GetExports<T, TMetadata>([CanBeNull] string contract = null);
    }
}