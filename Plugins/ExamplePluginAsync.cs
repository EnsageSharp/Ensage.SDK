// <copyright file="ExamplePluginAsync.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System;
    using System.ComponentModel.Composition;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Ensage.Common.Menu;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Input;
    using Ensage.SDK.Menu;
    using Ensage.SDK.Orbwalker;
    using Ensage.SDK.Orbwalker.Modes;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;
    using Ensage.SDK.TargetSelector;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public abstract class OrbwalkingModeAsync : OrbwalkingMode, IExecutableAsync
    {
        protected OrbwalkingModeAsync(IOrbwalker orbwalker)
            : base(orbwalker)
        {
        }

        protected TaskHandler Handler { get; set; }

        public override void Execute()
        {
            if (this.Handler?.IsRunning == true)
            {
                return;
            }

            this.Handler = UpdateManager.Run(this.ExecuteAsync);
        }

        public abstract Task ExecuteAsync(CancellationToken token);

        protected void Cancel()
        {
            this.Handler?.Cancel();
            this.Handler = null;
        }
    }

    public abstract class KeyPressOrbwalkingModeAsync : OrbwalkingModeAsync
    {
        private bool canExecute;

        protected KeyPressOrbwalkingModeAsync(IOrbwalker orbwalker, IInputManager input, Key key)
            : base(orbwalker)
        {
            this.Input = input;
            this.Key = key;
        }

        public override bool CanExecute
        {
            get
            {
                return this.canExecute;
            }
        }

        public Key Key { get; set; }

        private IInputManager Input { get; }

        protected override void OnActivate()
        {
            base.OnActivate();
            this.Input.KeyUp += this.OnKeyUp;
            this.Input.KeyDown += this.KeyDown;
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
            this.Input.KeyUp -= this.OnKeyUp;
            this.Input.KeyDown -= this.KeyDown;
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == this.Key)
            {
                this.canExecute = true;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == this.Key)
            {
                this.canExecute = false;
                this.Cancel();
            }
        }
    }

    public class ComboTest : KeyPressOrbwalkingModeAsync
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ComboTest(IOrbwalker orbwalker, IInputManager input)
            : base(orbwalker, input, Key.Space)
        {
        }

        public override async Task ExecuteAsync(CancellationToken token)
        {
            Log.Debug($"Delay 1000");
            await Task.Delay(1000, token);
        }
    }

    public static class Async
    {
        public static async Task OrbwalkToAsync(this IOrbwalker orbwalker, Unit target, CancellationToken token = default(CancellationToken))
        {
            while (!token.IsCancellationRequested)
            {
                if (!target.IsValid || !target.IsAlive)
                {
                    break;
                }

                if (orbwalker.OrbwalkTo(target))
                {
                    break;
                }

                await Task.Delay(50, token);
            }
        }
    }

    [ExportPlugin("Example Async Plugin")]
    public class AsyncPlugin : Plugin
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public AsyncPlugin()
        {
            UpdateManager.Subscribe(this.OnUpdate);
        }

        private TaskHandler Handler { get; set; }

        private async Task ExecuteAsync(CancellationToken token)
        {
            Log.Debug($"Delay 1000");
            await Task.Delay(1000, token);

            Log.Debug($"another Delay 1000");
            await Task.Delay(1000, token);
        }

        private void OnUpdate()
        {
            if (this.Handler?.IsRunning == true)
            {
                return;
            }

            this.Handler = UpdateManager.Run(this.ExecuteAsync);
        }
    }

    [ExportPlugin("Example Plugin Async", HeroId.npc_dota_hero_sniper)]
    public class ExamplePluginAsync : Plugin
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Lazy<IOrbwalkerManager> orbwalkerManager;

        private readonly Lazy<ITargetSelectorManager> targetManager;

        [ImportingConstructor]
        public ExamplePluginAsync([Import] Lazy<IOrbwalkerManager> orbManager, [Import] Lazy<ITargetSelectorManager> targetManager, [Import] Lazy<IInputManager> input)
        {
            this.Input = input;
            this.orbwalkerManager = orbManager;
            this.targetManager = targetManager;
        }

        public ComboTest ComboTest { get; set; }

        public ExamplePluginConfig Config { get; private set; }

        public Lazy<IInputManager> Input { get; }

        private IOrbwalker Orbwalker => this.orbwalkerManager.Value.Active;

        protected override void OnActivate()
        {
            this.ComboTest = new ComboTest(this.Orbwalker, this.Input.Value);
            this.Config = new ExamplePluginConfig();
            this.Orbwalker.RegisterMode(this.ComboTest);
        }

        protected override void OnDeactivate()
        {
            this.Orbwalker.UnregisterMode(this.ComboTest);
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