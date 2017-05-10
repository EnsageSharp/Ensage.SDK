// <copyright file="SamplePluginWithAsyncOrbwalkingMode.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Samples
{
    using System;
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

        private readonly Lazy<IInputManager> input;

        private readonly Lazy<IOrbwalkerManager> orbwalkerManager;

        private readonly Lazy<ITargetSelectorManager> targetManager;

        [ImportingConstructor]
        public SamplePluginWithAsyncOrbwalkingMode(
            [Import] Lazy<IOrbwalkerManager> orbManager,
            [Import] Lazy<ITargetSelectorManager> targetManager,
            [Import] Lazy<IInputManager> input)
        {
            this.input = input;
            this.orbwalkerManager = orbManager;
            this.targetManager = targetManager;
        }

        public SampleOrbwalkingModeWithAsync ComboTest { get; private set; }

        public SamplePluginConfig Config { get; private set; }

        private IOrbwalker Orbwalker => this.orbwalkerManager.Value.Active;

        protected override void OnActivate()
        {
            this.Config = new SamplePluginConfig();
            this.ComboTest = new SampleOrbwalkingModeWithAsync(this.Orbwalker, this.input.Value);

            this.Orbwalker.RegisterMode(this.ComboTest);
        }

        protected override void OnDeactivate()
        {
            this.Orbwalker.UnregisterMode(this.ComboTest);
            this.Config.Dispose();
        }
    }
}