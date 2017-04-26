// <copyright file="UpdateManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Reflection;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public static class UpdateManager
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly object SyncRoot = new object();

        static UpdateManager()
        {
            Game.OnIngameUpdate += OnUpdate;

            Handler = new ReflectionEventHandler<EventArgs>(typeof(Game), "OnPreUpdate");
            Handler.Subscribe(OnPreUpdate);
        }

        private static ReflectionEventHandler<EventArgs> Handler { get; }

        private static ImmutableSortedSet<UpdateHandler> ServiceHandlers { get; set; } = ImmutableSortedSet<UpdateHandler>.Empty;

        private static ImmutableSortedSet<UpdateHandler> UpdateHandlers { get; set; } = ImmutableSortedSet<UpdateHandler>.Empty;

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

        public static void Unsubscribe(Action callback)
        {
            lock (SyncRoot)
            {
                UpdateHandlers = Unsubscribe(UpdateHandlers, callback);
            }
        }

        /// <summary>
        /// Subscribes <paramref name="callback"/> to PreOnIngameUpdate with a call timeout of <paramref name="timeout"/>
        /// </summary>
        /// <param name="callback">callback</param>
        /// <param name="timeout">in ms</param>
        internal static void SubscribeService(Action callback, int timeout = 0)
        {
            lock (SyncRoot)
            {
                ServiceHandlers = Subscribe(ServiceHandlers, callback, timeout);
            }
        }

        internal static void UnsubscribeService(Action callback)
        {
            lock (SyncRoot)
            {
                ServiceHandlers = Unsubscribe(ServiceHandlers, callback);
            }
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

        private static ImmutableSortedSet<UpdateHandler> Subscribe(ImmutableSortedSet<UpdateHandler> handlers, Action callback, int timeout = 0)
        {
            var handler = handlers.FirstOrDefault(h => h.Callback == callback);
            if (handler != null && handler.Timeout != timeout)
            {
                Log.Debug($"Update Handler[{callback.Method}][{timeout}]");
                handler.Timeout = timeout;
                return handlers;
            }

            Log.Debug($"Create Handler[{callback.Method}][{timeout}]");
            return handlers.Add(new UpdateHandler(callback, timeout));
        }

        private static ImmutableSortedSet<UpdateHandler> Unsubscribe(ImmutableSortedSet<UpdateHandler> handlers, Action callback)
        {
            var handler = handlers.FirstOrDefault(h => h.Callback == callback);
            if (handler != null)
            {
                Log.Debug($"Remove Handler[{callback.Method}]");
                return handlers.Remove(handler);
            }

            return handlers;
        }
    }
}