// <copyright file="SamplePluginWithOrbOrbwalkingModeAsync.cs" company="Ensage">
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

    [ExportPlugin("SampleFrostArrowsOrbwalkingModeWithAsync", StartupMode.Manual)]
    public class SamplePluginWithOrbOrbwalkingModeAsync : Plugin
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Lazy<IInputManager> input;

        private readonly Lazy<IOrbwalkerManager> orbwalkerManager;

        private readonly Lazy<ITargetSelectorManager> targetManager;

        [ImportingConstructor]
        public SamplePluginWithOrbOrbwalkingModeAsync(
            [Import] Lazy<IOrbwalkerManager> orbManager,
            [Import] Lazy<ITargetSelectorManager> targetManager,
            [Import] Lazy<IInputManager> input)
        {
            this.input = input;
            this.orbwalkerManager = orbManager;
            this.targetManager = targetManager;
        }

        public SampleOrbOrbwalkingModeWithAsync ComboTest { get; private set; }

        public SamplePluginConfig Config { get; private set; }

        protected override void OnActivate()
        {
            this.ComboTest = new SampleOrbOrbwalkingModeWithAsync(this.orbwalkerManager.Value, this.input.Value, this.targetManager.Value);
            this.orbwalkerManager.Value.RegisterMode(this.ComboTest);
        }

        protected override void OnDeactivate()
        {
            this.orbwalkerManager.Value.UnregisterMode(this.ComboTest);
        }
    }
}