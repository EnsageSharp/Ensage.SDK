// <copyright file="ReflectionEventHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public class ReflectionEventHandler<TEventArgs>
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ReflectionEventHandler(Type type, string name, BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic)
        {
            this.Event = type?.GetEvent(name, flags);

            if (this.Event == null)
            {
                throw new Exception($"Failed to Reflect GetEvent({type?.Name}.{name})");
            }
        }

        private EventInfo Event { get; }

        private Dictionary<MethodInfo, Delegate> Handlers { get; } = new Dictionary<MethodInfo, Delegate>();

        public void Subscribe(Action<object, TEventArgs> action)
        {
            try
            {
                var method = action.Method;
                var handler = Delegate.CreateDelegate(this.Event.EventHandlerType, this, method);

                Log.Debug($"Add [{this.Event.Name}] {action.Method}");
                this.Event.GetAddMethod(true).Invoke(this.Event, new object[] { handler });
                this.Handlers.Add(method, handler);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public void Unsubscribe(Action<object, TEventArgs> action)
        {
            try
            {
                var method = action.Method;
                var handler = this.Handlers[method];

                Log.Debug($"Remove [{this.Event.Name}] {action.Target.GetType().Name}");
                this.Event.RemoveEventHandler(this, handler);
                this.Handlers.Remove(method);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}