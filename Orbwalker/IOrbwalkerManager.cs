// <copyright file="IOrbwalkerManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System;
    using System.Collections.Generic;

    using Ensage.SDK.Orbwalker.Config;
    using Ensage.SDK.Orbwalker.Metadata;

    using PlaySharp.Toolkit.Helper;

    public interface IOrbwalkerManager : IControllable
    {
        IOrbwalker Active { get; set; }

        OrbwalkerConfig Config { get; }

        IEnumerable<Lazy<IOrbwalker, IOrbwalkerMetadata>> Orbwalkers { get; }
    }
}