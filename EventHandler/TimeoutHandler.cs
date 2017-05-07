// <copyright file="TimeoutHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.EventHandler
{
    using System;

    public class TimeoutHandler : InvokeHandler
    {
        public TimeoutHandler(int timeout)
        {
            this.Timeout = timeout;
            this.NextUpdate = DateTime.Now;
        }

        public int Timeout { get; set; }

        protected bool HasTimeout
        {
            get
            {
                if (this.Timeout > 0)
                {
                    return DateTime.Now > this.NextUpdate;
                }

                return true;
            }
        }

        protected DateTime NextUpdate { get; set; }

        public override void Invoke(Action callback)
        {
            if (this.HasTimeout)
            {
                this.NextUpdate = DateTime.Now.AddMilliseconds(this.Timeout);
                callback.Invoke();
            }
        }

        public override string ToString()
        {
            return $"Timer[{this.Timeout}]";
        }
    }
}