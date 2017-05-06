// <copyright file="TraceUpdateHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.EventHandler
{
    using System;
    using System.Diagnostics;

    public class TraceUpdateHandler : TimeoutUpdateHandler
    {
        public TraceUpdateHandler(Action callback, int timeout = 0)
            : base(callback, timeout)
        {
        }

        public TimeSpan Time { get; private set; }

        private Stopwatch Stopwatch { get; } = new Stopwatch();

        public override bool Invoke()
        {
            if (!this.HasTimeout)
            {
                return false;
            }

            try
            {
                this.Stopwatch.Start();
                base.Invoke();
                this.Stopwatch.Stop();
            }
            finally
            {
                this.Time = this.Stopwatch.Elapsed;
                this.Stopwatch.Reset();
            }

            return true;
        }

        public override string ToString()
        {
            return $"[{this.Timeout}] {this.Callback?.Method.DeclaringType?.Name}.{this.Callback?.Method.Name}";
        }
    }
}