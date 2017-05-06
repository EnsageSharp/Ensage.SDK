// <copyright file="Messenger.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.EventHandler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public static class Messenger<TMessage>
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly object SyncRoot = new object();

        private static List<UpdateHandler<TMessage>> Handlers { get; } = new List<UpdateHandler<TMessage>>();

        public static void Publish(TMessage message)
        {
            foreach (var handler in Handlers.ToArray())
            {
                try
                {
                    handler.Invoke(message);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        public static void Subscribe(Action<TMessage> callback)
        {
            lock (SyncRoot)
            {
                if (Handlers.Any(e => e.Callback == callback))
                {
                    return;
                }

                var handler = new UpdateHandler<TMessage>(callback);

                Log.Debug($"Create {handler}");
                Handlers.Add(handler);
            }
        }

        public static void Unsubscribe(Action<TMessage> callback)
        {
            lock (SyncRoot)
            {
                var handler = Handlers.FirstOrDefault(e => e.Callback == callback);
                if (handler != null)
                {
                    Log.Debug($"Remove {handler}");
                    Handlers.Remove(handler);
                }
            }
        }
    }
}