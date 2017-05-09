// <copyright file="OrbwalkerPlugin.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System.ComponentModel.Composition;

    using Ensage.SDK.Orbwalker;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    [ExportPlugin("Orbwalker", StartupMode.Auto, description: "SDK Orbwalker Framework")]
    public class OrbwalkerPlugin : Plugin
    {
        [ImportingConstructor]
        public OrbwalkerPlugin(IOrbwalkerManager service)
        {
            this.Service = service;
        }

        public IOrbwalkerManager Service { get; }

        protected override void OnActivate()
        {
            this.Service.Activate();
        }

        protected override void OnDeactivate()
        {
            this.Service.Deactivate();
        }
    }
}