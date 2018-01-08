// <copyright file="ControllableService.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;

    using Ensage.SDK.Helpers;

    using Newtonsoft.Json;

    using PlaySharp.Toolkit.Helper;

    public abstract class ControllableService : IControllable, IDisposable
    {
        private bool disposed;

        protected ControllableService(bool activateOnCreation = true)
        {
            if (activateOnCreation)
            {
                UpdateManager.BeginInvoke(this.Activate);
            }
        }

        [JsonIgnore]
        public bool IsActive { get; private set; }

        public void Activate()
        {
            if (this.IsActive)
            {
                return;
            }

            this.IsActive = true;
            this.OnActivate();
        }

        public void Deactivate()
        {
            if (!this.IsActive)
            {
                return;
            }

            this.IsActive = false;
            this.OnDeactivate();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.Deactivate();
            }

            this.disposed = true;
        }

        protected virtual void OnActivate()
        {
        }

        protected virtual void OnDeactivate()
        {
        }
    }
}