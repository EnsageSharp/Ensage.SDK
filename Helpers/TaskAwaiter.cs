// <copyright file="TaskAwaiter.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.SDK.Threading;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public static class TaskAwaiter
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static TaskAwaiter()
        {
            UpdateManager.SubscribeService(Cleanup);
        }

        private static Dictionary<string, Task> Running { get; } = new Dictionary<string, Task>();

        public static void Await(string name, Func<CancellationToken, Task> factory)
        {
            if (IsRunning(name))
            {
                return;
            }

            Running[name] = null;
            EnsageDispatcher.BeginInvoke(() => { Running[name] = factory(CancellationToken.None); });
        }

        public static bool IsRunning(string name)
        {
            if (Running.ContainsKey(name))
            {
                return true;
            }

            return false;
        }

        private static void Cleanup()
        {
            foreach (var task in Running.Where(e => e.Value != null).ToArray())
            {
                if (task.Value.IsCompleted || task.Value.IsFaulted || task.Value.IsCanceled)
                {
                    Running.Remove(task.Key);
                }
            }
        }
    }
}