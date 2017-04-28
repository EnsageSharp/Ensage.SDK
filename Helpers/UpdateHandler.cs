// <copyright file="UpdateHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public class UpdateHandler
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public UpdateHandler(Action callback, int timeout = 0, bool enableTracing = false)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            this.Callback = callback;
            this.Timeout = timeout;
            this.EnableTracing = enableTracing;
        }

        public Action Callback { get; }

        public bool EnableTracing { get; set; }

        public bool HasTimeout
        {
            get
            {
                if (this.Timeout == 0)
                {
                    return true;
                }

                return DateTime.Now > this.NextUpdate;
            }
        }

        public DateTime NextUpdate { get; private set; }

        public int Timeout { get; set; }

        private Stopwatch TraceTimer { get; } = new Stopwatch();

        public override string ToString()
        {
            return $"{this.Callback?.Method.DeclaringType?.Name}.{this.Callback?.Method.Name}";
        }

        public void Update()
        {
            if (this.Timeout > 0)
            {
                this.NextUpdate = DateTime.Now.AddMilliseconds(this.Timeout);
            }

            if (this.EnableTracing)
            {
                this.TraceTimer.Start();
            }

            this.Callback?.Invoke();

            if (this.EnableTracing)
            {
                Log.Debug($"{this} {this.TraceTimer.ElapsedTicks}");
                this.TraceTimer.Reset();
            }
        }
    }
}