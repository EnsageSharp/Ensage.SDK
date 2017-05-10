// <copyright file="TaskHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.SDK.Helpers;

    using PlaySharp.Toolkit.Helper.Annotations;

    public class TaskHandler
    {
        private bool isRunning;

        public TaskHandler([NotNull] Func<CancellationToken, Task> factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.TaskFactory = factory;
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }

        public Task RunningTask { get; private set; }

        private Func<CancellationToken, Task> TaskFactory { get; }

        private CancellationTokenSource TokenSource { get; set; }

        public void Cancel(bool throwOnFirstException = false)
        {
            this.TokenSource?.Cancel(throwOnFirstException);
        }

        public void CancelAfter(TimeSpan delay)
        {
            this.TokenSource?.CancelAfter(delay);
        }

        public void CancelAfter(int millisecondsDelay)
        {
            this.TokenSource?.CancelAfter(millisecondsDelay);
        }

        public TaskHandler CreateCopy()
        {
            return new TaskHandler(this.TaskFactory);
        }

        public void RunAsync()
        {
            if (this.isRunning)
            {
                return;
            }

            this.isRunning = true;

            this.TokenSource = new CancellationTokenSource();
            this.RunningTask = UpdateManager
                .Factory
                .StartNew(() => this.TaskFactory(this.TokenSource.Token), this.TokenSource.Token)
                .ContinueWith(this.Complete);
        }

        private void Complete(Task<Task> task)
        {
            this.TokenSource = null;
            this.RunningTask = null;

            this.isRunning = false;
        }
    }
}