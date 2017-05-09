// <copyright file="TraceHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Handlers
{
    using System;
    using System.Diagnostics;

    public class TraceHandler : TimeoutHandler
    {
        public TraceHandler(int timeout = 0)
            : base(timeout)
        {
        }

        public TimeSpan Time { get; private set; }

        private Stopwatch Stopwatch { get; } = new Stopwatch();

        public override void Invoke(Action callback)
        {
            if (!this.HasTimeout)
            {
                return;
            }

            try
            {
                this.Stopwatch.Start();
                callback.Invoke();
                this.Stopwatch.Stop();
            }
            finally
            {
                this.Time = this.Stopwatch.Elapsed;
                this.Stopwatch.Reset();
            }

            this.NextUpdate = DateTime.Now.AddMilliseconds(this.Timeout);
        }

        public override string ToString()
        {
            return $"Trace[{this.Timeout}]";
        }
    }
}