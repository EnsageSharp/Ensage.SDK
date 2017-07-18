// <copyright file="TimeoutHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Handlers
{
    using System;

    public class TimeoutHandler : InvokeHandler
    {
        private int timeout;

        public TimeoutHandler(int timeout, bool fromNow = false)
        {
            this.Timeout = timeout;
            this.NextUpdate = fromNow ? DateTime.Now.AddMilliseconds(this.Timeout) : DateTime.Now;
        }

        public int Timeout
        {
            get
            {
                return this.timeout;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                this.timeout = value;
            }
        }

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

        public override bool Invoke(Action callback)
        {
            if (this.HasTimeout)
            {
                this.NextUpdate = DateTime.Now.AddMilliseconds(this.Timeout);
                callback.Invoke();
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"Timer[{this.Timeout}]";
        }
    }
}