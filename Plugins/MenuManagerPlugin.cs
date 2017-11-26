// <copyright file="OrbwalkerPlugin.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Menu;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    [ExportPlugin("MenuManager", StartupMode.Auto, priority: 0, description: "SDK MenuManager Framework")]
    public class MenuManagerPlugin : Plugin
    {
        [ImportingConstructor]
        public MenuManagerPlugin([Import] Lazy<MenuManager> service)
        {
            this.Service = service;
        }

        public Lazy<MenuManager> Service { get; }

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