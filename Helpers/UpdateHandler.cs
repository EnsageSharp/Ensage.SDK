// <copyright file="UpdateHandler.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;

    public class UpdateHandler
    {
        public UpdateHandler(Action callback, int timeout = 0)
        {
            this.Callback = callback;
            this.Timeout = timeout;
        }

        public Action Callback { get; }

        public bool HasTimeout
        {
            get
            {
                if (this.Timeout == 0)
                {
                    return true;
                }

                return DateTime.Now > this.NextUpdate;
            }
        }

        public DateTime NextUpdate { get; private set; }

        public int Timeout { get; set; }

        public void Update()
        {
            if (this.Timeout > 0)
            {
                this.NextUpdate = DateTime.Now.AddMilliseconds(this.Timeout);
            }

            this.Callback?.Invoke();
        }
    }
}