// <copyright file="ITargetSelectorManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector
{
    using System;
    using System.Collections.Generic;

    using Ensage.SDK.TargetSelector.Metadata;

    using PlaySharp.Toolkit.Helper;

    public interface ITargetSelectorManager : IControllable
    {
        ITargetSelector Active { get; }

        IEnumerable<Lazy<ITargetSelector, ITargetSelectorMetadata>> Selectors { get; }
    }
}