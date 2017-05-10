// <copyright file="SamplePluginWithAsync.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Samples
{
    using System.Reflection;
    using System.Threading.Tasks;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    [ExportPlugin("SamplePluginWithAsync", StartupMode.Manual)]
    public class SamplePluginWithAsync : Plugin
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected override void OnActivate()
        {
            UpdateManager.BeginInvoke(this.TestLoop);
        }

        private async void TestLoop()
        {
            while (this.IsActive)
            {
                Log.Debug($"Loop");
                await Task.Delay(1000);
            }
        }
    }
}