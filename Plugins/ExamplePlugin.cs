// <copyright file="ExamplePlugin.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Ensage.Common.Menu;
    using Ensage.SDK.Menu;
    using Ensage.SDK.Orbwalker;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;
    using Ensage.SDK.TargetSelector;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    [ExportPlugin("Example Plugin Async", HeroId.npc_dota_hero_sniper)]
    public class ExamplePluginAsync : Plugin, IOrbwalkingModeAsync
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Lazy<IOrbwalkerManager> orbwalkerManager;

        private readonly Lazy<ITargetSelectorManager> targetManager;

        [ImportingConstructor]
        public ExamplePluginAsync([Import] Lazy<IOrbwalkerManager> orbManager, [Import] Lazy<ITargetSelectorManager> targetManager)
        {
            this.orbwalkerManager = orbManager;
            this.targetManager = targetManager;
        }

        public bool CanExecute
        {
            get
            {
                return this.Config.Key;
            }
        }

        public ExamplePluginConfig Config { get; private set; }

        private IOrbwalker Orbwalker => this.orbwalkerManager.Value.Active;

        private ITargetSelector TargetSelector => this.targetManager.Value.Active;

        public async Task ExecuteAsync(CancellationToken token)
        {
            await Task.Delay(200, token);

            var target = this.TargetSelector.GetTargets().FirstOrDefault();

            // pre attack
            if (this.Orbwalker.OrbwalkTo(target))
            {
                // move / attacked
            }
        }

        protected override void OnActivate()
        {
            this.Config = new ExamplePluginConfig();
            this.Orbwalker.RegisterMode(this);
        }

        protected override void OnDeactivate()
        {
            this.Orbwalker.UnregisterMode(this);
            this.Config.Dispose();
        }
    }

    public class ExamplePluginConfig : IDisposable
    {
        public ExamplePluginConfig()
        {
            this.Factory = MenuFactory.Create("Example Plugin");
            this.UseExploit = this.Factory.Item("Use Exploit", true);
            this.Key = this.Factory.Item("Exploit Key", new KeyBind('K'));
        }

        public MenuFactory Factory { get; }

        public MenuItem<KeyBind> Key { get; }

        public MenuItem<bool> UseExploit { get; }

        public void Dispose()
        {
            this.Factory?.Dispose();
        }
    }
}