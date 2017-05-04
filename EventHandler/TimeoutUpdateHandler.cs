// <copyright file="TimeoutUpdateHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.EventHandler
{
    using System;

    public class TimeoutUpdateHandler : UpdateHandler
    {
        public TimeoutUpdateHandler(Action callback, int timeout)
            : base(callback)
        {
            this.Timeout = timeout;
            this.NextUpdate = DateTime.Now.AddMilliseconds(this.Timeout);
        }

        public int Timeout { get; set; }

        private DateTime NextUpdate { get; set; }

        public override void Invoke()
        {
            if (DateTime.Now > this.NextUpdate)
            {
                this.NextUpdate = DateTime.Now.AddMilliseconds(this.Timeout);
                base.Invoke();
            }
        }

        public override string ToString()
        {
            return $"TimeoutHandler[{this.Timeout}][{this.Callback?.Method.DeclaringType?.Name}.{this.Callback?.Method.Name}]";
        }
    }
}