// <copyright file="IOrbwalkerManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System;
    using System.Collections.Generic;

    using Ensage.SDK.Orbwalker.Config;
    using Ensage.SDK.Orbwalker.Metadata;

    public interface IOrbwalkerManager : IOrbwalker
    {
        IOrbwalker Active { get; set; }

        OrbwalkerConfig Config { get; }

        IEnumerable<IOrbwalkingMode> CustomOrbwalkingModes { get; }

        IEnumerable<Lazy<IOrbwalker, IOrbwalkerMetadata>> Orbwalkers { get; }

        IEnumerable<Lazy<IOrbwalkingMode, IOrbwalkingModeMetadata>> OrbwalkingModes { get; }

        void RegisterMode(IOrbwalkingMode mode);

        void UnregisterMode(IOrbwalkingMode mode);
    }
}