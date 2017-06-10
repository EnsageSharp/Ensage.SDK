// <copyright file="DummyTargetSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Modes
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.Common.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service;
    using Ensage.SDK.TargetSelector.Metadata;

    [ExportTargetSelector("Target Dummy")]
    public class DummyTargetSelector : ControllableService, ITargetSelector
    {
        [ImportingConstructor]
        public DummyTargetSelector([Import] IServiceContext context)
        {
            this.Owner = context.Owner;
        }

        private Unit Owner { get; }

        public IEnumerable<Unit> GetTargets()
        {
            return EntityManager<Unit>.Entities.Where(e => e.ClassId == ClassId.CDOTA_Unit_TargetDummy).OrderBy(e => e.Distance2D(this.Owner));
        }
    }
}