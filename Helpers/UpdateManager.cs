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

        internal static List<IUpdateHandler> Handlers { get; } = new List<IUpdateHandler>();

        internal static List<IUpdateHandler> ServiceHandlers { get; } = new List<IUpdateHandler>();

        /// <summary>
        /// Subscribes <paramref name="callback"/> to OnIngameUpdate with a call timeout of <paramref name="timeout"/>
        /// </summary>
        /// <param name="callback">callback</param>
        /// <param name="timeout">in ms</param>
        public static void Subscribe(Action callback, int timeout = 0)
        {
            Subscribe(Handlers, callback, timeout);
        }

        /// <summary>
        /// Subscribes <paramref name="callback"/> to OnPreIngameUpdate with a call timeout of <paramref name="timeout"/>
        /// </summary>
        /// <param name="callback">callback</param>
        /// <param name="timeout">in ms</param>
        public static void SubscribeService(Action callback, int timeout = 0)
        {
            Subscribe(ServiceHandlers, callback, timeout);
        }

        public static void Unsubscribe(Action callback)
        {
            Unsubscribe(Handlers, callback);
        }

        public static void UnsubscribeService(Action callback)
        {
            Unsubscribe(ServiceHandlers, callback);
        }

        private static void OnPreUpdate(EventArgs eventArgs)
        {
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

        private static void Subscribe(ICollection<IUpdateHandler> handlers, Action callback, int timeout = 0)
        {
            var handler = handlers.FirstOrDefault(h => h.Callback == callback);
            if (handler == null)
            {
                if (timeout > 0)
                {
                    handler = new UpdateHandler(callback, new TimeoutHandler(timeout));
                }
                else
                {
                    handler = new UpdateHandler(callback, InvokeHandler.Default);
                }

                Log.Debug($"Create {handler}");
                handlers.Add(handler);
            }
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