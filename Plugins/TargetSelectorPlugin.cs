// <copyright file="TargetSelectorPlugin.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;
    using Ensage.SDK.TargetSelector;

    [ExportPlugin("Target Selector", StartupMode.Auto, priority: 0, description: "SDK Target Selector Framework")]
    public class TargetSelectorPlugin : Plugin
    {
        [ImportingConstructor]
        public TargetSelectorPlugin([Import] Lazy<ITargetSelectorManager> service)
        {
            this.Service = service;
        }

        public Lazy<ITargetSelectorManager> Service { get; }

        protected override void OnActivate()
        {
            this.Service.Value.Activate();
        }

        protected override void OnDeactivate()
        {
            this.Service.Value.Deactivate();
        }
    }
}