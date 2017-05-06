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

        public bool HasTimeout
        {
            get
            {
                return DateTime.Now > this.NextUpdate;
            }
        }

        public int Timeout { get; set; }

        private DateTime NextUpdate { get; set; }

        public override bool Invoke()
        {
            if (this.HasTimeout)
            {
                this.NextUpdate = DateTime.Now.AddMilliseconds(this.Timeout);
                return base.Invoke();
            }

            return false;
        }

        public override string ToString()
        {
            return $"TimeoutHandler[{this.Timeout}][{this.Callback?.Method.DeclaringType?.Name}.{this.Callback?.Method.Name}]";
        }
    }
}