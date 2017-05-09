// <copyright file="OrbwalkingModeAsync.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker.Modes
{
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service;

    public abstract class OrbwalkingModeAsync : OrbwalkingMode, IExecutableAsync
    {
        protected OrbwalkingModeAsync(IOrbwalker orbwalker)
            : base(orbwalker)
        {
        }

        protected TaskHandler Handler { get; set; }

        public override void Execute()
        {
            if (this.Handler == null)
            {
                this.Handler = UpdateManager.Run(this.ExecuteAsync);
            }

            this.Handler.RunAsync();
        }

        public abstract Task ExecuteAsync(CancellationToken token);

        protected void Cancel()
        {
            this.Handler?.Cancel();
        }
    }
}