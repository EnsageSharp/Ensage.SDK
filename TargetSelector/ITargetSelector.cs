using System.Collections.Generic;

namespace Ensage.SDK.TargetSelector
{
    public interface ITargetSelector
    {
        Unit GetClosestUnitToMouse(Team team = Team.Undefined);
        IEnumerable<Unit> GetUnitsInRange(float range, Team team = Team.Undefined);
        Unit GetWeakestAttackUnit();
    }
}