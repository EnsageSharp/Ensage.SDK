// <copyright file="UpdateManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.EventHandler;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public static class UpdateManager
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static UpdateManager()
        {
            Game.OnIngameUpdate += OnUpdate;
            Game.OnPreIngameUpdate += OnPreUpdate;
        }

        internal static List<IUpdateHandler> PreUpdateHandlers { get; } = new List<IUpdateHandler>();

        internal static List<IUpdateHandler> UpdateHandlers { get; } = new List<IUpdateHandler>();

        /// <summary>
        /// Subscribes <paramref name="callback"/> to OnIngameUpdate with a call timeout of <paramref name="timeout"/>
        /// </summary>
        /// <param name="callback">callback</param>
        /// <param name="timeout">in ms</param>
        public static void Subscribe(Action callback, int timeout = 0)
        {
            // Trace(UpdateHandlers, callback, timeout);
            Subscribe(UpdateHandlers, callback, timeout);
        }

        /// <summary>
        /// Subscribes <paramref name="callback"/> to OnPreIngameUpdate with a call timeout of <paramref name="timeout"/>
        /// </summary>
        /// <param name="callback">callback</param>
        /// <param name="timeout">in ms</param>
        public static void SubscribeService(Action callback, int timeout = 0)
        {
            // Trace(PreUpdateHandlers, callback, timeout);
            Subscribe(PreUpdateHandlers, callback, timeout);
        }

        public static void Unsubscribe(Action callback)
        {
            Unsubscribe(UpdateHandlers, callback);
        }

        public static void UnsubscribeService(Action callback)
        {
            Unsubscribe(PreUpdateHandlers, callback);
        }

        private static void OnPreUpdate(EventArgs eventArgs)
        {
            foreach (var handler in PreUpdateHandlers.ToArray())
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
            foreach (var handler in UpdateHandlers.ToArray())
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

        private static void Subscribe(List<IUpdateHandler> handlers, Action callback, int timeout = 0)
        {
            var handler = handlers.FirstOrDefault(h => h.Callback == callback);

            var timeoutHandler = handler as TimeoutUpdateHandler;
            if (timeoutHandler != null && timeoutHandler.Timeout != timeout)
            {
                timeoutHandler.Timeout = timeout;

                Log.Debug($"Update {timeoutHandler}");
                return;
            }

            if (timeout > 0)
            {
                handler = new TimeoutUpdateHandler(callback, timeout);

                Log.Debug($"Create {handler}");
                handlers.Add(handler);
                return;
            }

            handler = new UpdateHandler(callback);

            Log.Debug($"Create {handler}");
            handlers.Add(handler);
        }

        private static void Trace(List<IUpdateHandler> handlers, Action callback, int timeout = 0)
        {
            var handler = handlers.FirstOrDefault(h => h.Callback == callback);
            if (handler == null)
            {
                handler = new TraceUpdateHandler(callback, timeout);

                Log.Debug($"Create {handler}");
                handlers.Add(handler);
            }
        }

        private static void Unsubscribe(List<IUpdateHandler> handlers, Action callback)
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