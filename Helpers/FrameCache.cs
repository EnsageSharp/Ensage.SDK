// <copyright file="FrameCache.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;

    using PlaySharp.Toolkit.Helper.Annotations;

    public class FrameCache<TValue> : IDisposable
        where TValue : class
    {
        private bool disposed;

        private TValue value;

        public FrameCache([NotNull] Func<TValue> valueFactory, int timeout = 0)
        {
            this.Factory = valueFactory;
            UpdateManager.SubscribeService(this.OnFrameChanged, timeout);
        }

        public event EventHandler FrameChanged;

        public bool HasValue
        {
            get
            {
                return this.value != null;
            }
        }

        public TValue Value
        {
            get
            {
                if (this.value == null)
                {
                    this.value = this.Factory();
                }

                return this.value;
            }
        }

        private Func<TValue> Factory { get; }

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
                UpdateManager.UnsubscribeService(this.OnFrameChanged);
            }

            this.disposed = true;
        }

        private void OnFrameChanged()
        {
            this.value = null;
            this.FrameChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}