// <copyright file="TraceHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Handlers
{
    using System;
    using System.Diagnostics;

    using Ensage.SDK.Helpers;

    public class TraceHandler : TimeoutHandler
    {
        public TraceHandler(int timeout = 0)
            : base(timeout)
        {
        }

        public TimeSpan Time { get; private set; }

        public CircularBuffer<TimeSpan> TimeHistory { get; } = new CircularBuffer<TimeSpan>(100);

        private Stopwatch Stopwatch { get; } = new Stopwatch();

        public override bool Invoke(Action callback)
        {
            if (!this.HasTimeout)
            {
                return false;
            }

            this.Stopwatch.Start();

            try
            {
                callback.Invoke();
            }
            finally
            {
                this.Stopwatch.Stop();

                this.Time = this.Stopwatch.Elapsed;
                this.TimeHistory.Enqueue(this.Stopwatch.Elapsed);

                this.Stopwatch.Reset();
            }

            this.NextUpdate = DateTime.Now.AddMilliseconds(this.Timeout);
            return true;
        }

        public override string ToString()
        {
            return $"Trace[{this.Timeout}]";
        }
    }
}