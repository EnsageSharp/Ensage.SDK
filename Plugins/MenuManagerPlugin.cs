// <copyright file="OrbwalkerPlugin.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    [ExportPlugin("MenuManager", StartupMode.Auto, priority: -1, description: "SDK MenuManager Framework")]
    public class MenuManagerPlugin : Plugin
    {
        [ImportingConstructor]
        public MenuManagerPlugin([Import] Lazy<IServiceContext> context)
        {
            this.Context = context;
        }

        public Lazy<IServiceContext> Context { get; }

        protected override void OnActivate()
        {
            this.Context.Value.MenuManager.Activate();
        }

        protected override void OnDeactivate()
        {
            this.Context.Value.MenuManager.Deactivate();
        }
    }
}