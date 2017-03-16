// <copyright file="ITargetSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector
{
    using System.Collections.Generic;

    public interface ITargetSelector
    {
        Unit GetClosestUnitToMouse(Team team = Team.Undefined);

        IEnumerable<Unit> GetUnitsInRange(float range, Team team = Team.Undefined);

        Unit GetWeakestAttackUnit();
    }
}