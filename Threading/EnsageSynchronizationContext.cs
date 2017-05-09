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

    using Ensage.SDK.Helpers;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    internal class EnsageSynchronizationContext : SynchronizationContext
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly object SyncRoot = new object();

        private static EnsageSynchronizationContext instance;

        private readonly ConcurrentQueue<KeyValuePair<SendOrPostCallback, object>> queue = new ConcurrentQueue<KeyValuePair<SendOrPostCallback, object>>();

        private EnsageSynchronizationContext()
        {
            UpdateManager.Subscribe(this.Run);
        }

        public static EnsageSynchronizationContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new EnsageSynchronizationContext();
                        }
                    }
                }

                return instance;
            }
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            this.queue.Enqueue(new KeyValuePair<SendOrPostCallback, object>(d, state));
        }

        private void Run()
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