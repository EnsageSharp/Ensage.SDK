// <copyright file="InvokeHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.EventHandler
{
    using System;

    public class InvokeHandler
    {
        private static InvokeHandler instance;

        public static InvokeHandler Default
        {
            get
            {
                if (instance == null)
                {
                    instance = new InvokeHandler();
                }

                return instance;
            }
        }

        public virtual void Invoke(Action callback)
        {
            callback.Invoke();
        }

        public override string ToString()
        {
            return "Handler";
        }
    }

    public class InvokeHandler<TEventArgs>
    {
        private static InvokeHandler<TEventArgs> instance;

        public static InvokeHandler<TEventArgs> Default
        {
            get
            {
                if (instance == null)
                {
                    instance = new InvokeHandler<TEventArgs>();
                }

                return instance;
            }
        }

        public virtual void Invoke(Action<TEventArgs> callback, TEventArgs args)
        {
            callback.Invoke(args);
        }

        public override string ToString()
        {
            return $"Handler<{typeof(TEventArgs).Name}>";
        }
    }
}