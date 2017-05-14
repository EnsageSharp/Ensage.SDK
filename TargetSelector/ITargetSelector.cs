// <copyright file="ITargetSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector
{
    using System.Collections.Generic;

    using PlaySharp.Toolkit.Helper;

    public interface ITargetSelector : IControllable
    {
        IEnumerable<Unit> GetTargets();
    }
}