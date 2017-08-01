// <copyright file="IAbilityDetector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Generic;

    public interface IAbilityDetector
    {
        event EventHandler<AbilityEventArgs> AbilityCasted;

        event EventHandler<AbilityEventArgs> AbilityCastStarted;

        IEnumerable<DetectedAbility> ActiveAbilities { get; }
    }
}