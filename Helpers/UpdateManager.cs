// <copyright file="UpdateManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.EventHandler;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public static class UpdateManager
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly object SyncRoot = new object();

        static UpdateManager()
        {
            Game.OnIngameUpdate += OnUpdate;

            Handler = new ReflectionEventHandler<Game, EventArgs>("OnPreUpdate");
            Handler.Subscribe(OnPreUpdate);
        }

        private static ReflectionEventHandler<Game, EventArgs> Handler { get; }

        private static ImmutableArray<IUpdateHandler> PreUpdateHandlers { get; set; } = ImmutableArray<IUpdateHandler>.Empty;

        private static ImmutableArray<IUpdateHandler> UpdateHandlers { get; set; } = ImmutableArray<IUpdateHandler>.Empty;

        /// <summary>
        /// Subscribes <paramref name="callback"/> to OnIngameUpdate with a call timeout of <paramref name="timeout"/>
        /// </summary>
        /// <param name="callback">callback</param>
        /// <param name="timeout">in ms</param>
        public static void Subscribe(Action callback, int timeout = 0)
        {
            lock (SyncRoot)
            {
                UpdateHandlers = Subscribe(UpdateHandlers, callback, timeout);
            }
        }

        /// <summary>
        /// Subscribes <paramref name="callback"/> to OnPreIngameUpdate with a call timeout of <paramref name="timeout"/>
        /// </summary>
        /// <param name="callback">callback</param>
        /// <param name="timeout">in ms</param>
        public static void SubscribeService(Action callback, int timeout = 0)
        {
            lock (SyncRoot)
            {
                PreUpdateHandlers = Subscribe(PreUpdateHandlers, callback, timeout);
            }
        }

        public static void Trace(Action callback)
        {
            lock (SyncRoot)
            {
                var handler = UpdateHandlers.FirstOrDefault(h => h.Callback == callback);
                if (handler == null)
                {
                    handler = new TraceUpdateHandler(callback);

                    Log.Debug($"Create {handler}");
                    UpdateHandlers = UpdateHandlers.Add(handler);
                }
            }
        }

        public static void Unsubscribe(Action callback)
        {
            lock (SyncRoot)
            {
                UpdateHandlers = Unsubscribe(UpdateHandlers, callback);
            }
        }

        public static void UnsubscribeService(Action callback)
        {
            lock (SyncRoot)
            {
                PreUpdateHandlers = Unsubscribe(PreUpdateHandlers, callback);
            }
        }

        private static void OnPreUpdate(object sender, EventArgs eventArgs)
        {
            foreach (var handler in PreUpdateHandlers)
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
            foreach (var handler in UpdateHandlers)
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

        private static ImmutableArray<IUpdateHandler> Subscribe(ImmutableArray<IUpdateHandler> handlers, Action callback, int timeout = 0)
        {
            var handler = handlers.FirstOrDefault(h => h.Callback == callback);

            var timeoutHandler = handler as TimeoutUpdateHandler;
            if (timeoutHandler != null && timeoutHandler.Timeout != timeout)
            {
                timeoutHandler.Timeout = timeout;

                Log.Debug($"Update {timeoutHandler}");
                return handlers;
            }

            if (timeout > 0)
            {
                handler = new TimeoutUpdateHandler(callback, timeout);

                Log.Debug($"Create {handler}");
                return handlers.Add(handler);
            }

            handler = new UpdateHandler(callback);

            Log.Debug($"Create {handler}");
            return handlers.Add(handler);
        }

        private static ImmutableArray<IUpdateHandler> Unsubscribe(ImmutableArray<IUpdateHandler> handlers, Action callback)
        {
            var handler = handlers.FirstOrDefault(h => h.Callback == callback);
            if (handler != null)
            {
                Log.Debug($"Remove {handler}");
                return handlers.Remove(handler);
            }

            return handlers;
        }
    }
}