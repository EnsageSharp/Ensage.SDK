// <copyright file="UpdateManager.cs" company="Ensage">
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

    using Ensage.SDK.Handlers;
    using Ensage.SDK.Threading;

    using log4net;

    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    public static class UpdateManager
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static UpdateManager()
        {
            Context = new EnsageSynchronizationContext();

            SynchronizationContext.SetSynchronizationContext(Context);

            Factory = new TaskFactory(
                CancellationToken.None,
                TaskCreationOptions.DenyChildAttach,
                TaskContinuationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());

            Subscribe(Context.ProcessCallbacks);

            Game.OnIngameUpdate += OnUpdate;
            Game.OnPreIngameUpdate += OnPreUpdate;
        }

        public static EnsageSynchronizationContext Context { get; }

        public static TaskFactory Factory { get; }

        internal static List<IUpdateHandler> Handlers { get; } = new List<IUpdateHandler>();

        internal static List<IUpdateHandler> InvokeHandlers { get; } = new List<IUpdateHandler>();

        internal static List<IUpdateHandler> ServiceHandlers { get; } = new List<IUpdateHandler>();

        public static void BeginInvoke(Action callback, int timeout = 0)
        {
            if (timeout < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout));
            }

            if (timeout == 0)
            {
                Context.Post(state => callback(), null);
                return;
            }

            InvokeHandlers.Add(new UpdateHandler(callback, new TimeoutHandler(timeout, true)));
        }

        public static TaskHandler Run([NotNull] Func<CancellationToken, Task> factory, bool restart = true, bool autostart = true)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            var task = new TaskHandler(factory, restart);

            if (autostart)
            {
                task.RunAsync();
            }

            return task;
        }

        /// <summary>
        ///     Subscribes <paramref name="callback" /> to OnIngameUpdate with a call timeout of <paramref name="timeout" />
        /// </summary>
        /// <param name="callback">callback</param>
        /// <param name="timeout">in ms</param>
        /// <param name="isEnabled">startup IsEnabled state</param>
        public static IUpdateHandler Subscribe(Action callback, int timeout = 0, bool isEnabled = true)
        {
            return Subscribe(Handlers, callback, timeout, isEnabled);
        }

        /// <summary>
        ///     Subscribes <paramref name="callback" /> to OnPreIngameUpdate with a call timeout of <paramref name="timeout" />
        /// </summary>
        /// <param name="callback">callback</param>
        /// <param name="timeout">in ms</param>
        /// <param name="isEnabled">startup IsEnabled state</param>
        public static IUpdateHandler SubscribeService(Action callback, int timeout = 0, bool isEnabled = true)
        {
            return Subscribe(ServiceHandlers, callback, timeout, isEnabled);
        }

        public static void Unsubscribe(Action callback)
        {
            Unsubscribe(Handlers, callback);
        }

        public static void Unsubscribe(IUpdateHandler handler)
        {
            Handlers.Remove(handler);
        }

        public static void UnsubscribeService(IUpdateHandler handler)
        {
            ServiceHandlers.Remove(handler);
        }

        public static void UnsubscribeService(Action callback)
        {
            Unsubscribe(ServiceHandlers, callback);
        }

        private static void OnPreUpdate(EventArgs eventArgs)
        {
            SynchronizationContext.SetSynchronizationContext(Context);

            foreach (var handler in ServiceHandlers.ToArray())
            {
                try
                {
                    handler.Invoke();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        private static void OnUpdate(EventArgs args)
        {
            SynchronizationContext.SetSynchronizationContext(Context);

            foreach (var handler in InvokeHandlers.ToArray())
            {
                try
                {
                    if (handler.Invoke())
                    {
                        InvokeHandlers.Remove(handler);
                    }
                }
                catch (Exception e)
                {
                    InvokeHandlers.Remove(handler);
                    Log.Error(e);
                }
            }

            foreach (var handler in Handlers.ToArray())
            {
                try
                {
                    handler.Invoke();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        private static IUpdateHandler Subscribe(ICollection<IUpdateHandler> handlers, Action callback, int timeout = 0, bool isEnabled = true)
        {
            if (timeout < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout));
            }

            var handler = handlers.FirstOrDefault(h => h.Callback == callback);
            if (handler == null)
            {
                if (timeout > 0)
                {
                    handler = new UpdateHandler(callback, new TimeoutHandler(timeout), isEnabled);
                }
                else
                {
                    handler = new UpdateHandler(callback, InvokeHandler.Default, isEnabled);
                }

                Log.Debug($"Create {handler}");
                handlers.Add(handler);
            }

            return handler;
        }

        private static void Unsubscribe(ICollection<IUpdateHandler> handlers, Action callback)
        {
            var handler = handlers.FirstOrDefault(h => h.Callback == callback);
            if (handler != null)
            {
                Log.Debug($"Remove {handler}");
                handlers.Remove(handler);
            }
        }
    }
}