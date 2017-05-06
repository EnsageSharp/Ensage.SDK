// <copyright file="MostSpellDamageSelector.cs" company="Ensage">
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

    [ExportTargetSelector("Most Spell Damage")]
    public class MostSpellDamageSelector : SelectorBase
    {
        [ImportingConstructor]
        public MostSpellDamageSelector([Import] IServiceContext context)
            : base(context)
        {
        }

        protected override IEnumerable<Unit> GetTargetsImpl()
        {
            var team = this.Owner.Team;

            return EntityManager<Hero>.Entities.Where(e => e.IsAlive && !e.IsIllusion && e.Team != team).OrderByDescending(e => e.GetSpellAmplification()).ToArray();
        }
    }
}