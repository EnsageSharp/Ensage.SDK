// <copyright file="EnsageTaskScheduler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Threading
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class EnsageTaskScheduler : TaskScheduler
    {
        private static readonly object SyncRoot = new object();

        private static volatile EnsageTaskScheduler instance;

        private EnsageTaskScheduler()
        {
            this.Factory = new TaskFactory(
                CancellationToken.None,
                TaskCreationOptions.DenyChildAttach,
                TaskContinuationOptions.None,
                this);
        }

        public static EnsageTaskScheduler Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new EnsageTaskScheduler();
                        }
                    }
                }

                return instance;
            }
        }

        public TaskFactory Factory { get; }

        public override int MaximumConcurrencyLevel => 1;

        public Task Run(Func<Task> function, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Factory.StartNew(function, cancellationToken);
        }

        public Task Run(Action Action, CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Factory.StartNew(Action, cancellationToken);
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return null;
        }

        protected override void QueueTask(Task task)
        {
            EnsageSynchronizationContext.Instance.Post(state => task.Start(this), task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if (SynchronizationContext.Current == EnsageSynchronizationContext.Instance)
            {
                return this.TryExecuteTask(task);
            }

            return false;
        }
    }
}