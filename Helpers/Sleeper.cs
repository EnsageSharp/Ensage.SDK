// <copyright file="Sleeper.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;

    public sealed class Sleeper
    {
        public Sleeper(TimeSpan timeout)
        {
            this.Timeout = timeout;
            UpdateManager.Subscribe(this.OnUpdate);
        }

        /// <param name="timeout">
        ///     <see cref="Elapsed" /> timeout in Milliseconds
        /// </param>
        public Sleeper(int timeout)
        {
            this.Timeout = TimeSpan.FromMilliseconds(timeout);
            UpdateManager.Subscribe(this.OnUpdate);
        }

        public event EventHandler Elapsed;

        public bool HasTimeout
        {
            get
            {
                return this.GetTicks() > this.NextTimeout;
            }
        }

        public TimeSpan Timeout { get; set; }

        private long NextTimeout { get; set; }

        public void Reset()
        {
            this.NextTimeout = this.GetTicks() + this.Timeout.Ticks;
            this.Elapsed?.Invoke(this, EventArgs.Empty);
        }

        private long GetTicks()
        {
            return DateTime.Now.Ticks;
        }

        private void OnUpdate()
        {
            if (this.HasTimeout)
            {
                this.Reset();
            }
        }
    }
}