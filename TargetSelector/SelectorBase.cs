// <copyright file="SelectorBase.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector
{
    using System.Collections.Generic;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service;

    public abstract class SelectorBase : ControllableService, ITargetSelector
    {
        protected SelectorBase(IServiceContext context)
        {
            this.Owner = context.Owner;
            this.Targets = new FrameCache<IEnumerable<Unit>>(this.GetTargetsImpl);
        }

        protected Unit Owner { get; }

        protected FrameCache<IEnumerable<Unit>> Targets { get; }

        public virtual IEnumerable<Unit> GetTargets()
        {
            return this.Targets.Value;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Targets?.Dispose();
            }

            base.Dispose(disposing);
        }

        protected abstract IEnumerable<Unit> GetTargetsImpl();
    }
}