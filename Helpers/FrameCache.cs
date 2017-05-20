// <copyright file="FrameCache.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;

    using PlaySharp.Toolkit.Helper.Annotations;

    public class FrameCache<TValue> : IDisposable
    {
        private bool disposed;

        private TValue value;

        public FrameCache([NotNull] Func<TValue> valueFactory, int timeout = 0)
        {
            this.Factory = valueFactory;
            UpdateManager.SubscribeService(this.OnFrameChanged, timeout);
        }

        public event EventHandler FrameChanged;

        public bool HasValue { get; private set; }

        public TValue Value
        {
            get
            {
                if (!this.HasValue)
                {
                    this.value = this.Factory();
                    this.HasValue = true;
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
            this.HasValue = false;
            this.FrameChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}