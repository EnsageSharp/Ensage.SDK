// <copyright file="Timer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;

    public sealed class Timer : IDisposable
    {
        public Timer(TimeSpan timeout, bool autoStart = false)
            : this(timeout.Ticks, autoStart)
        {
        }

        /// <param name="timeout">
        ///     <see cref="Elapsed" /> timeout in Milliseconds
        /// </param>
        /// <param name="autoStart">start timer on creation</param>
        public Timer(long timeout, bool autoStart = false)
        {
            this.Timeout = timeout;
            this.NextTimeout = this.GetNextTimeout();

            if (autoStart)
            {
                this.Start();
            }
        }

        public event EventHandler Elapsed;

        public bool HasTimeout
        {
            get
            {
                return this.Ticks > this.NextTimeout;
            }
        }

        public long NextTimeout { get; private set; }

        public long Ticks
        {
            get
            {
                return UpdateManager.Ticks;
            }
        }

        public long Timeout { get; set; }

        public void Dispose()
        {
            this.Stop();
        }

        public void Reset()
        {
            this.NextTimeout = this.GetNextTimeout();
        }

        public void Start()
        {
            UpdateManager.Subscribe(this.OnUpdate);
        }

        public void Stop()
        {
            UpdateManager.Unsubscribe(this.OnUpdate);
        }

        private long GetNextTimeout()
        {
            return this.Ticks + this.Timeout;
        }

        private void OnUpdate()
        {
            if (!this.HasTimeout)
            {
                return;
            }

            try
            {
                this.Elapsed?.Invoke(this, EventArgs.Empty);
            }
            finally
            {
                this.Reset();
            }
        }
    }
}