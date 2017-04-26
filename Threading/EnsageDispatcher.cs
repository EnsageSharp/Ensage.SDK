// <copyright file="EnsageDispatcher.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Threading
{
    using System;
    using System.Threading;

    internal class EnsageDispatcher : IDisposable
    {
        private static readonly object SyncRoot = new object();

        private static volatile EnsageDispatcher instance;

        private bool disposed;

        private EnsageDispatcher()
        {
            Game.OnUpdate += this.UpdateDispatcher;
            Game.OnIngameUpdate += this.IngameUpdateDispatcher;
        }

        public event GameIngameUpdate OnIngameUpdate;

        public event GameUpdate OnUpdate;

        public static EnsageDispatcher Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new EnsageDispatcher();
                        }
                    }
                }

                return instance;
            }
        }

        public void BeginInvoke(Action action)
        {
            EnsageSynchronizationContext.Instance.Post(state => action(), null);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void InvokeEvent(Action action)
        {
            var gameContext = EnsageSynchronizationContext.Instance;
            var context = SynchronizationContext.Current;

            try
            {
                SynchronizationContext.SetSynchronizationContext(gameContext);
                action();
                gameContext.RunOnCurrentThread();
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(context);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                Game.OnUpdate -= this.UpdateDispatcher;
                Game.OnIngameUpdate -= this.IngameUpdateDispatcher;
            }

            this.disposed = true;
        }

        private void IngameUpdateDispatcher(EventArgs args)
        {
            this.InvokeEvent(() => this.OnIngameUpdate?.Invoke(EventArgs.Empty));
        }

        private void UpdateDispatcher(EventArgs args)
        {
            this.InvokeEvent(() => this.OnUpdate?.Invoke(EventArgs.Empty));
        }
    }
}