// <copyright file="MostAttackDamageSelector.cs" company="Ensage">
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

    [ExportTargetSelector("Most Attack Damage")]
    public class MostAttackDamageSelector : SelectorBase
    {
        [ImportingConstructor]
        public MostAttackDamageSelector([Import] IServiceContext context)
            : base(context)
        {
        }

        protected override IEnumerable<Unit> GetTargetsImpl()
        {
            var team = this.Owner.Team;
            return EntityManager<Hero>.Entities.Where(e => e.IsVisible && e.IsAlive && !e.IsIllusion && e.Team != team).OrderByDescending(e => e.GetAttackDamage(this.Owner)).ToArray();
        }
    }
}