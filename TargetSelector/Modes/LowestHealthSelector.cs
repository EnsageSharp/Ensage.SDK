// <copyright file="LowestHealthSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Modes
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service;
    using Ensage.SDK.TargetSelector.Metadata;

    [ExportTargetSelector("Lowest Health")]
    public class LowestHealthSelector : SelectorBase
    {
        [ImportingConstructor]
        public LowestHealthSelector([Import] IServiceContext context)
            : base(context)
        {
        }

        protected override IEnumerable<Unit> GetTargetsImpl()
        {
            var team = this.Owner.Team;
            return EntityManager<Hero>.Entities.Where(e => e.IsAlive && e.Team != team && e.Health > 0).OrderBy(e => e.Health).ToArray();
        }
    }
}