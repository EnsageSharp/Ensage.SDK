// <copyright file="IServiceManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.Collections.Generic;

    using PlaySharp.Toolkit.Helper;

    public interface IServiceManager<TService, TServiceMetadata> : IControllable
        where TService : class
    {
        TService Active { get; set; }

        IEnumerable<Lazy<TService, TServiceMetadata>> Services { get; }
    }

    public interface IServiceManager<TService> : IControllable
        where TService : class
    {
        TService Active { get; set; }

        IEnumerable<Lazy<TService>> Services { get; }
    }
}