// <copyright file="SelectorBase.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector
{
    using System;
    using System.Collections.Generic;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service;

    public abstract class SelectorBase : ITargetSelector, IDisposable
    {
        private bool disposed;

        protected SelectorBase(IServiceContext context)
        {
            this.Owner = context.Owner;
            this.Targets = new FrameCache<IEnumerable<Unit>>(this.GetTargetsImpl);
        }

        protected Hero Owner { get; }

        protected FrameCache<IEnumerable<Unit>> Targets { get; }

        public virtual void Activate()
        {
        }

        public virtual void Deactivate()
        {
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual IEnumerable<Unit> GetTargets()
        {
            return this.Targets.Value;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.Targets.Dispose();
            }

            this.disposed = true;
        }

        protected abstract IEnumerable<Unit> GetTargetsImpl();
    }
}