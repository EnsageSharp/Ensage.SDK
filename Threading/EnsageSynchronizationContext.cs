// <copyright file="EnsageSynchronizationContext.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Threading
{
    using System.Collections.Generic;
    using System.Threading;

    public class EnsageSynchronizationContext : SynchronizationContext
    {
        private static readonly object SyncRoot = new object();

        private static EnsageSynchronizationContext instance;

        private Queue<KeyValuePair<SendOrPostCallback, object>> queue =
            new Queue<KeyValuePair<SendOrPostCallback, object>>();

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
            lock (SyncRoot)
            {
                this.queue.Enqueue(new KeyValuePair<SendOrPostCallback, object>(d, state));
            }
        }

        internal void RunOnCurrentThread()
        {
            KeyValuePair<SendOrPostCallback, object>[] items;

            lock (SyncRoot)
            {
                items = this.queue.ToArray();
                this.queue = new Queue<KeyValuePair<SendOrPostCallback, object>>();
            }

            foreach (var item in items)
            {
                item.Key(item.Value);
            }
        }
    }
}