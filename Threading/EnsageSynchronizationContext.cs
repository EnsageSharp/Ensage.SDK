// <copyright file="EnsageSynchronizationContext.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Threading
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;

    internal class EnsageSynchronizationContext : SynchronizationContext
    {
        private static readonly object SyncRoot = new object();

        private static EnsageSynchronizationContext instance;

        private readonly ConcurrentQueue<KeyValuePair<SendOrPostCallback, object>> queue =
            new ConcurrentQueue<KeyValuePair<SendOrPostCallback, object>>();

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

        public override SynchronizationContext CreateCopy()
        {
            return new EnsageSynchronizationContext();
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            this.queue.Enqueue(new KeyValuePair<SendOrPostCallback, object>(d, state));
        }

        internal void RunOnCurrentThread()
        {
            KeyValuePair<SendOrPostCallback, object> workItem;

            while (!this.queue.IsEmpty && this.queue.TryDequeue(out workItem))
            {
                workItem.Key(workItem.Value);
            }
        }
    }
}