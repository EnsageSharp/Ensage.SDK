// <copyright file="SamplePluginWithAsync.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Samples
{
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    [ExportPlugin("SamplePluginWithAsync")]
    public class SamplePluginWithAsync : Plugin
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public SamplePluginWithAsync()
        {
            this.Handler = UpdateManager.Run(this.ExecuteAsync, false);
        }

        private TaskHandler Handler { get; }

        protected override void OnActivate()
        {
            var source = new CancellationTokenSource();
            var tk = source.Token;

            Log.Debug($"UpdateManager.Factory.StartNew");
            var task = UpdateManager.Factory.StartNew(
                async () =>
                {
                    Log.Debug($"UpdateManager.Factory: pre-inner");
                    await Task.Delay(1000, tk);
                    Log.Debug($"UpdateManager.Factory: post-inner");
                },
                tk);

            Log.Debug($"UpdateManager.Run");
            var handler = UpdateManager.Run(
                async token =>
                {
                    Log.Debug($"UpdateManager.Run: pre-inner");
                    await Task.Delay(2000, tk);
                    Log.Debug($"UpdateManager.Run: post-inner");
                });

            UpdateManager.Subscribe(this.OnUpdate);
        }

        protected override void OnDeactivate()
        {
            UpdateManager.Unsubscribe(this.OnUpdate);
            this.Handler?.Cancel();
        }

        private async Task ExecuteAsync(CancellationToken token)
        {
            Log.Debug($"Plugin.ExecuteAsync: Delay 1000");
            await Task.Delay(1000, token);

            Log.Debug($"Plugin.ExecuteAsync: another Delay 1000");
            await Task.Delay(1000, token);
        }

        private void OnUpdate()
        {
            this.Handler.RunAsync();
        }
    }
}