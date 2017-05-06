// <copyright file="NearMouseSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Modes
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service;
    using Ensage.SDK.TargetSelector.Metadata;

    [ExportTargetSelector("Near Mouse")]
    public class NearMouseSelector : SelectorBase
    {
        [ImportingConstructor]
        public NearMouseSelector([Import] IServiceContext context, [Import] TargetSelectorConfig parent)
            : base(context)
        {
            this.Config = new NearMouseConfig(parent.Factory);
        }

        public NearMouseConfig Config { get; }

        protected override IEnumerable<Unit> GetTargetsImpl()
        {
            var pos = Game.MousePosition;
            var team = this.Owner.Team;

            return EntityManager<Hero>.Entities.Where(e => e.IsAlive && !e.IsIllusion && e.Team != team).Where(e => e.Position.Distance(pos) < this.Config.Range.Value.Value)
                                      .OrderBy(e => e.Position.Distance(pos)).ToArray();
        }
    }
}