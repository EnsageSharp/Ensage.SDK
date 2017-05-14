// <copyright file="EnsageSynchronizationContext.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Threading
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public sealed class EnsageSynchronizationContext : SynchronizationContext
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ConcurrentQueue<KeyValuePair<SendOrPostCallback, object>> queue = new ConcurrentQueue<KeyValuePair<SendOrPostCallback, object>>();

        internal EnsageSynchronizationContext()
        {
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            this.queue.Enqueue(new KeyValuePair<SendOrPostCallback, object>(d, state));
        }

        internal void ProcessCallbacks()
        {
            KeyValuePair<SendOrPostCallback, object> workItem;

            while (!this.queue.IsEmpty && this.queue.TryDequeue(out workItem))
            {
                try
                {
                    workItem.Key(workItem.Value);
                }
                catch (Exception e)
                {
                    Log.Warn(e);
                }
            }
        }
    }
}