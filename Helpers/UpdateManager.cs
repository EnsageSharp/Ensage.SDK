// <copyright file="UpdateManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public static class UpdateManager
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static UpdateManager()
        {
            Game.OnIngameUpdate += OnUpdate;

            Handler = new ReflectionEventHandler<EventArgs>(typeof(Game), "PreUpdate");
            Handler.Attach(OnPreUpdate);
        }

        private static ReflectionEventHandler<EventArgs> Handler { get; }

        private static List<UpdateHandler> ServiceHandlers { get; } = new List<UpdateHandler>();

        private static List<UpdateHandler> UpdateHandlers { get; } = new List<UpdateHandler>();

        /// <summary>
        /// Subscribes <paramref name="callback"/> to OnIngameUpdate with a call timeout of <paramref name="timeout"/>
        /// </summary>
        /// <param name="callback">callback</param>
        /// <param name="timeout">in ms</param>
        public static void Subscribe(Action callback, int timeout = 0)
        {
            Subscribe(UpdateHandlers, callback, timeout);
        }

        public static void Unsubscribe(Action callback)
        {
            Unsubscribe(UpdateHandlers, callback);
        }

        /// <summary>
        /// Subscribes <paramref name="callback"/> to PreOnIngameUpdate with a call timeout of <paramref name="timeout"/>
        /// </summary>
        /// <param name="callback">callback</param>
        /// <param name="timeout">in ms</param>
        internal static void SubscribeService(Action callback, int timeout = 0)
        {
            Subscribe(ServiceHandlers, callback, timeout);
        }

        internal static void UnsubscribeService(Action callback)
        {
            Unsubscribe(ServiceHandlers, callback);
        }

        private static void OnPreUpdate(object sender, EventArgs args)
        {
            foreach (var handler in ServiceHandlers.Where(h => h.HasTimeout))
            {
                try
                {
                    handler.Update();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        private static void OnUpdate(EventArgs args)
        {
            foreach (var handler in UpdateHandlers.Where(h => h.HasTimeout))
            {
                try
                {
                    handler.Update();
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        private static void Subscribe(ICollection<UpdateHandler> handlers, Action callback, int timeout = 0)
        {
            var handler = handlers.FirstOrDefault(h => h.Callback == callback);
            if (handler != null && handler.Timeout != timeout)
            {
                Log.Debug($"Update Handler[{callback.Target}][{timeout}]");
                handler.Timeout = timeout;
                return;
            }

            Log.Debug($"Create Handler[{callback.Target}][{timeout}]");
            handlers.Add(new UpdateHandler(callback, timeout));
        }

        private static void Unsubscribe(ICollection<UpdateHandler> handlers, Action callback)
        {
            var handler = handlers.FirstOrDefault(h => h.Callback == callback);
            if (handler != null)
            {
                Log.Debug($"Remove Handler[{callback.Target}]");
                handlers.Remove(handler);
            }
        }
    }
}