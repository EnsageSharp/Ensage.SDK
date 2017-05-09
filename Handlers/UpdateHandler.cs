// <copyright file="UpdateHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Handlers
{
    using System;

    public class UpdateHandler : IUpdateHandler
    {
        public UpdateHandler(Action callback, InvokeHandler executor)
            : this($"{callback?.Method.DeclaringType?.Name}.{callback?.Method.Name}", callback, executor)
        {
        }

        public UpdateHandler(string name, Action callback, InvokeHandler executor)
        {
            this.Name = name;
            this.Callback = callback;
            this.Executor = executor;
        }

        public Action Callback { get; }

        public InvokeHandler Executor { get; set; }

        public string Name { get; }

        public virtual void Invoke()
        {
            this.Executor.Invoke(this.Callback);
        }

        public override string ToString()
        {
            return $"{this.Executor}[{this.Name}]";
        }
    }

    public class UpdateHandler<TEventArgs> : IUpdateHandler<TEventArgs>
    {
        public UpdateHandler(Action<TEventArgs> callback, InvokeHandler<TEventArgs> executor)
        {
            this.Name = $"{callback?.Method.DeclaringType?.Name}.{callback?.Method.Name}";
            this.Callback = callback;
            this.Executor = executor;
        }

        public Action<TEventArgs> Callback { get; }

        public InvokeHandler<TEventArgs> Executor { get; set; }

        public string Name { get; }

        public virtual void Invoke(TEventArgs args)
        {
            this.Executor.Invoke(this.Callback, args);
        }

        public override string ToString()
        {
            return $"{this.Executor}[{this.Name}]";
        }
    }
}