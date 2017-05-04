// <copyright file="IUpdateHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.EventHandler
{
    using System;

    public interface IUpdateHandler
    {
        Action Callback { get; }

        void Invoke();
    }

    public interface IUpdateHandler<in TEventArgs>
    {
        Action<TEventArgs> Callback { get; }

        void Invoke(TEventArgs args);
    }
}