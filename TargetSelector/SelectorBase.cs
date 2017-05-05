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
            UpdateManager.SubscribeService(this.OnClear);
        }

        protected Hero Owner { get; }

        protected IReadOnlyList<Hero> Targets { get; set; }

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

        public abstract IEnumerable<Unit> GetTargets();

        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                UpdateManager.UnsubscribeService(this.OnClear);
            }

            this.disposed = true;
        }

        private void OnClear()
        {
            this.Targets = null;
        }
    }
}