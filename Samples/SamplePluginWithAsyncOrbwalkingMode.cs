// <copyright file="SamplePluginWithAsyncOrbwalkingMode.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Samples
{
    using System.ComponentModel.Composition;
    using System.Reflection;

    using Ensage.SDK.Input;
    using Ensage.SDK.Orbwalker;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;
    using Ensage.SDK.TargetSelector;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    [ExportPlugin("SamplePluginWithAsyncOrbwalkingMode", StartupMode.Manual)]
    public class SamplePluginWithAsyncOrbwalkingMode : Plugin
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IInputManager input;

        private readonly IOrbwalkerManager orbwalkerManager;

        private readonly ITargetSelectorManager targetManager;

        [ImportingConstructor]
        public SamplePluginWithAsyncOrbwalkingMode(
            [Import] IServiceContext context)
        {
            this.Context = context;
            this.input = context.Input;
            this.orbwalkerManager = context.Orbwalker;
            this.targetManager = context.TargetSelector;
        }

        public SampleOrbwalkingModeWithAsync ComboTest { get; private set; }

        public SamplePluginConfig Config { get; private set; }

        public IServiceContext Context { get; }

        protected override void OnActivate()
        {
            this.Config = new SamplePluginConfig();
            this.ComboTest = new SampleOrbwalkingModeWithAsync(this.Context);

            this.orbwalkerManager.RegisterMode(this.ComboTest);
        }

        protected override void OnDeactivate()
        {
            this.orbwalkerManager.UnregisterMode(this.ComboTest);
            this.Config.Dispose();
        }
    }
}