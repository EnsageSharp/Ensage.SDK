// <copyright file="TargetSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service;

    [ExportTargetSelector("SDK")]
    public class TargetSelector : ITargetSelector
    {
        [ImportingConstructor]
        public TargetSelector(IServiceContext context)
        {
            this.Context = context;
        }

        private IServiceContext Context { get; }

        public Unit GetClosestUnitToMouse(float range)
        {
            return this.GetClosestUnitToMouse(Team.Undefined, range);
        }

        public Unit GetClosestUnitToMouse(Team team = Team.Undefined, float range = 800)
        {
            var mousePosition = Game.MousePosition;
            var query = team == Team.Undefined
                            ? this.GetValidTargetEnemyTeam<Unit>(this.Context.Owner.Team)
                            : this.GetValidTargeTeam<Unit>(team);
            query = query.Where(x => x.Distance2D(mousePosition) < 800);
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

        private IEnumerable<T> GetValidTargeTeam<T>(Team team) where T : Unit, new()
        {
            return EntityManager<T>.Entities.Where(x => x.IsAlive && x.Team == team && x.IsRealUnit());
        }

        private IEnumerable<T> GetValidTargetEnemyTeam<T>(Team team) where T : Unit, new()
        {
            return EntityManager<T>.Entities.Where(x => x.IsAlive && x.Team != team && x.IsRealUnit());
        }
    }
}