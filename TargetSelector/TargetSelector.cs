// <copyright file="TargetSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector
{
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.SDK.Abilities;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Service;

    [ExportTargetSelector("SDK")]
    public class TargetSelector : ITargetSelector
    {
        public TargetSelector(IEnsageServiceContext context)
        {
            this.Context = context;
        }

        private IEnsageServiceContext Context { get; }

        public IEnumerable<Unit> GetAOEUnits(IActiveAbility ability)
        {
            return null;
        }

        public Unit GetClosestUnitToMouse(Team team = Team.Undefined)
        {
            var mousePosition = Game.MousePosition;
            var query = team == Team.Undefined
                            ? this.GetValidTargetEnemyTeam<Unit>(this.Context.Owner.Team)
                            : this.GetValidTargeTeam<Unit>(team);
            return query.OrderBy(x => x.Distance2D(mousePosition)).FirstOrDefault();
        }

        public IEnumerable<Unit> GetUnitsInRange(float range, Team team = Team.Undefined)
        {
            var contextOwner = this.Context.Owner;
            var query = team == Team.Undefined
                            ? this.GetValidTargetEnemyTeam<Unit>(contextOwner.Team)
                            : this.GetValidTargeTeam<Unit>(team);
            return query.Where(x => x.Distance2D(contextOwner) <= range);
        }

        public Unit GetWeakestAttackUnit()
        {
            var contextOwner = this.Context.Owner;
            var attackRange = contextOwner.AttackRange();
            var unitsInRange = this.GetUnitsInRange(attackRange);
            var result = unitsInRange.OrderBy(x => x.Health * (1.0f + x.DamageResist)).FirstOrDefault();
            return result;
        }

        private ParallelQuery<T> GetValidTargeTeam<T>(Team team) where T : Unit, new()
        {
            return
                ObjectManager.GetEntitiesParallel<T>()
                             .Where(x => x.IsValid && x.IsAlive && x.Team == team && x.IsRealUnit());
        }

        private ParallelQuery<T> GetValidTargetEnemyTeam<T>(Team team) where T : Unit, new()
        {
            return
                ObjectManager.GetEntitiesParallel<T>()
                             .Where(x => x.IsValid && x.IsAlive && x.Team != team && x.IsRealUnit());
        }
    }
}