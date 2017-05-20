// <copyright file="IOrbwalkerManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using Ensage.SDK.Orbwalker.Config;

    public interface IOrbwalkerManager : IOrbwalker
    {
        IOrbwalker Active { get; set; }

        OrbwalkerConfig Config { get; }

        void RegisterMode(IOrbwalkingMode mode);

        void UnregisterMode(IOrbwalkingMode mode);
    }
}