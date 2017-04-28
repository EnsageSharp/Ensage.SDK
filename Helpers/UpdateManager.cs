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

        private static ImmutableArray<UpdateHandler> PreUpdateHandlers { get; set; } = ImmutableArray<UpdateHandler>.Empty;

        private static ImmutableArray<UpdateHandler> UpdateHandlers { get; set; } = ImmutableArray<UpdateHandler>.Empty;

        /// <summary>
        /// Enables Performace tracing for <paramref name="assembly"/>
        /// </summary>
        /// <param name="assembly">Target Assembly</param>
        public static void EnableTracing(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    var handler = UpdateHandlers.FirstOrDefault(h => h.Callback.Method == method);
                    if (handler != null)
                    {
                        handler.EnableTracing = true;
                    }
                }
            }
        }

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
        /// Subscribes <paramref name="callback"/> to OnPreIngameUpdate with a call timeout of <paramref name="timeout"/>
        /// </summary>
        /// <param name="callback">callback</param>
        /// <param name="timeout">in ms</param>
        internal static void SubscribeService(Action callback, int timeout = 0)
        {
            lock (SyncRoot)
            {
                PreUpdateHandlers = Subscribe(PreUpdateHandlers, callback, timeout);
            }
        }

        internal static void UnsubscribeService(Action callback)
        {
            lock (SyncRoot)
            {
                PreUpdateHandlers = Unsubscribe(PreUpdateHandlers, callback);
            }
        }

        private static void OnPreUpdate(object sender, EventArgs args)
        {
            foreach (var handler in PreUpdateHandlers.Where(h => h.HasTimeout))
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

        private static ImmutableArray<UpdateHandler> Subscribe(ImmutableArray<UpdateHandler> handlers, Action callback, int timeout = 0)
        {
            var handler = handlers.FirstOrDefault(h => h.Callback == callback);
            if (handler != null && handler.Timeout != timeout)
            {
                Log.Debug($"Update Handler[{timeout}][{callback.Method.DeclaringType}.{callback.Method.Name}]");
                handler.Timeout = timeout;
                return handlers;
            }

            Log.Debug($"Create Handler[{timeout}][{callback.Method.DeclaringType}.{callback.Method.Name}]");
            return handlers.Add(new UpdateHandler(callback, timeout));
        }

        private static ImmutableArray<UpdateHandler> Unsubscribe(ImmutableArray<UpdateHandler> handlers, Action callback)
        {
            var handler = handlers.FirstOrDefault(h => h.Callback == callback);
            if (handler != null)
            {
                Log.Debug($"Remove Handler[{callback.Method.DeclaringType}.{callback.Method.Name}]");
                return handlers.Remove(handler);
            }

            return handlers;
        }
    }
}