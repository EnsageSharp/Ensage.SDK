// <copyright file="OrbwalkerPlugin.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Orbwalker;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    [ExportPlugin("Orbwalker", StartupMode.Auto, priority: 0, description: "SDK Orbwalker Framework")]
    public class OrbwalkerPlugin : Plugin
    {
        [ImportingConstructor]
        public OrbwalkerPlugin([Import] Lazy<IOrbwalkerManager> service)
        {
            this.Service = service;
        }

        public Lazy<IOrbwalkerManager> Service { get; }

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