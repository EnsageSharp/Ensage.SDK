// <copyright file="UpdateHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.EventHandler
{
    using System;

    public class UpdateHandler : IUpdateHandler
    {
        public UpdateHandler(Action callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            this.Callback = callback;
        }

        public Action Callback { get; }

        public virtual bool Invoke()
        {
            this.Callback?.Invoke();
            return true;
        }

        public override string ToString()
        {
            return $"{this.Callback?.Method.DeclaringType?.Name}.{this.Callback?.Method.Name}";
        }
    }

    public class UpdateHandler<TEventArgs> : IUpdateHandler<TEventArgs>
    {
        public UpdateHandler(Action<TEventArgs> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            this.Callback = callback;
        }

        public Action<TEventArgs> Callback { get; }

        public virtual bool Invoke(TEventArgs args)
        {
            this.Callback?.Invoke(args);
            return true;
        }

        public override string ToString()
        {
            return $"Handler[{this.Callback?.Method.DeclaringType?.Name}.{this.Callback?.Method.Name}]";
        }
    }
}