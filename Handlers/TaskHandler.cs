// <copyright file="TaskHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Handlers
{
    using System;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.SDK.Helpers;

    using log4net;

    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    public class TaskHandler
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool isRunning;

        public TaskHandler([NotNull] Func<CancellationToken, Task> factory, bool restart = false)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.TaskFactory = factory;
            this.Restart = restart;
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }

        public Task RunningTask { get; private set; }

        private bool Restart { get; }

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
            return new TaskHandler(this.TaskFactory, this.Restart);
        }

        public void RunAsync()
        {
            if (this.isRunning)
            {
                return;
            }

            this.isRunning = true;

            this.TokenSource = new CancellationTokenSource();
            this.RunningTask = UpdateManager.Factory.StartNew(
                async () =>
                {
                    try
                    {
                        do
                        {
                            await this.TaskFactory(this.TokenSource.Token);
                            await Task.Delay(10, this.TokenSource.Token);
                        }
                        while (this.Restart && !this.TokenSource.IsCancellationRequested);
                    }
                    catch (TaskCanceledException)
                    {
                        // canceled
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                    finally
                    {
                        this.TokenSource = null;
                        this.RunningTask = null;

                        this.isRunning = false;
                    }
                });
        }
    }
}