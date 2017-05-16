// <copyright file="IUpdateHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Handlers
{
    using System;

    public interface IUpdateHandler
    {
        Action Callback { get; }

        InvokeHandler Executor { get; set; }

        bool IsEnabled { get; set; }

        string Name { get; }

        bool Invoke();
    }

    public interface IUpdateHandler<TEventArgs>
    {
        Action<TEventArgs> Callback { get; }

        InvokeHandler<TEventArgs> Executor { get; set; }

        bool IsEnabled { get; set; }

        string Name { get; }

        bool Invoke(TEventArgs args);
    }
}