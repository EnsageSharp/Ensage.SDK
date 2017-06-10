namespace Ensage.SDK.TargetSelector
{
    using System;
    using System.Collections.Generic;

    using PlaySharp.Toolkit.Helper;

    public interface IServiceManager<TService> : IControllable
        where TService : class, IControllable
    {
        TService Active { get; set; }

        IEnumerable<Lazy<TService>> Services { get; }
    }
}