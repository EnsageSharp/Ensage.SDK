// <copyright file="IOrbwalkerManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System;
    using System.Collections.Generic;

    using Ensage.SDK.Orbwalker.Metadata;
    using Ensage.SDK.Service;

    public interface IOrbwalkerManager : IServiceManager<IOrbwalker, IOrbwalkerMetadata>, IOrbwalker
    {
        SDKConfig.OrbwalkerConfig Config { get; }

        IEnumerable<IOrbwalkingMode> CustomOrbwalkingModes { get; }

        IEnumerable<Lazy<IOrbwalkingMode, IOrbwalkingModeMetadata>> OrbwalkingModes { get; }

        void RegisterMode(IOrbwalkingMode mode);

        void UnregisterMode(IOrbwalkingMode mode);
    }
}