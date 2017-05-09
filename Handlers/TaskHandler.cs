// <copyright file="TaskHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Handlers
{
    using System;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.SDK.Threading;

    using log4net;

    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    public class TaskHandler
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool isRunning;

        public TaskHandler([NotNull] Func<CancellationToken, Task> factory, [NotNull] CancellationTokenSource token = default(CancellationTokenSource))
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            this.TaskFactory = factory;
            this.Token = token;
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }

        public Task Task { get; private set; }

        public Func<CancellationToken, Task> TaskFactory { get; }

        public CancellationTokenSource Token { get; }

        public void Cancel()
        {
            this.Token.Cancel();
        }

        public void RunAsync()
        {
            this.isRunning = true;
            EnsageDispatcher.BeginInvoke(this.RunOnce);
        }

        private async void RunOnce()
        {
            try
            {
                this.Task = this.TaskFactory(this.Token.Token);
                await this.Task.ConfigureAwait(true);
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            finally
            {
                this.isRunning = false;
            }
        }
    }
}