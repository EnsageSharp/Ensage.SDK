// <copyright file="SampleOrbwalkingModeWithAsync.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Samples
{
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Ensage.SDK.Orbwalker.Modes;
    using Ensage.SDK.Service;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public class SampleOrbwalkingModeWithAsync : KeyPressOrbwalkingModeAsync
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public SampleOrbwalkingModeWithAsync(IServiceContext context)
            : base(context, Key.Space)
        {
        }

        public override async Task ExecuteAsync(CancellationToken token)
        {
            Log.Debug($"Delay 1000");
            await Task.Delay(1000, token);
        }
    }
}