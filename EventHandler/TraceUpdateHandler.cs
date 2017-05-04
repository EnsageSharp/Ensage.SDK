// <copyright file="TraceUpdateHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.EventHandler
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public class TraceUpdateHandler : UpdateHandler
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public TraceUpdateHandler(Action callback)
            : base(callback)
        {
        }

        private Stopwatch Stopwatch { get; } = new Stopwatch();

        public override void Invoke()
        {
            try
            {
                this.Stopwatch.Restart();
                base.Invoke();
                this.Stopwatch.Stop();
            }
            finally
            {
                Log.Debug($"{this} {this.Stopwatch.Elapsed}");
            }
        }

        public override string ToString()
        {
            return $"TraceHandler[{this.Callback?.Method.DeclaringType?.Name}.{this.Callback?.Method.Name}]";
        }
    }
}