// <copyright file="EnsageDispatcher.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Threading
{
    using System;
    using System.Threading;

    public static class EnsageDispatcher
    {
        public static void BeginInvoke(Action action)
        {
            SynchronizationContext.Current.Post(state => action(), null);
        }
    }
}