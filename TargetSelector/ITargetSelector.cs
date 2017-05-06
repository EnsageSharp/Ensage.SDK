// <copyright file="ITargetSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector
{
    using System.Collections.Generic;

    public interface ITargetSelector
    {
        void Activate();

        void Deactivate();

        IEnumerable<Unit> GetTargets();
    }
}