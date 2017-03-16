using System.Collections.Generic;
using System.Linq;
using Ensage.SDK.Abilities;
using Ensage.SDK.Extensions;
using Ensage.SDK.Service;

namespace Ensage.SDK.TargetSelector
{
    [ExportTargetSelector("SDK")]
    public class TargetSelector : ITargetSelector
    {
        public TargetSelector(IEnsageServiceContext context)
        {
            Context = context;
        }

        private IEnsageServiceContext Context { get; }


        public Unit GetClosestUnitToMouse(Team team = Team.Undefined)
        {
            var mousePosition = Game.MousePosition;
            var query = team == Team.Undefined
                ? GetValidTargetEnemyTeam<Unit>(Context.Owner.Team)
                : GetValidTargeTeam<Unit>(team);
            return query.OrderBy(x => x.Distance2D(mousePosition)).FirstOrDefault();
        }

        public IEnumerable<Unit> GetUnitsInRange(float range, Team team = Team.Undefined)
        {
            var contextOwner = Context.Owner;
            var query = team == Team.Undefined
                ? GetValidTargetEnemyTeam<Unit>(contextOwner.Team)
                : GetValidTargeTeam<Unit>(team);
            return query.Where(x => x.Distance2D(contextOwner) <= range);
        }

        public Unit GetWeakestAttackUnit()
        {
            var contextOwner = Context.Owner;
            var attackRange = contextOwner.AttackRange();
            var unitsInRange = GetUnitsInRange(attackRange);
            var result = unitsInRange.OrderBy(x => x.Health * (1.0f + x.DamageResist)).FirstOrDefault();
            return result;
        }

        public IEnumerable<Unit> GetAOEUnits(IActiveAbility ability)
        {
            return null;
        }


        private ParallelQuery<T> GetValidTargetEnemyTeam<T>(Team team) where T : Unit, new()
        {
            return
                ObjectManager.GetEntitiesParallel<T>()
                             .Where(x => x.IsValid && x.IsAlive && x.Team != team && x.IsRealUnit());
        }

        private ParallelQuery<T> GetValidTargeTeam<T>(Team team) where T : Unit, new()
        {
            return
                ObjectManager.GetEntitiesParallel<T>()
                             .Where(x => x.IsValid && x.IsAlive && x.Team == team && x.IsRealUnit());
        }
    }
}