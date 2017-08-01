// <copyright file="OrbwalkerManager.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Orbwalker.Config;
    using Ensage.SDK.Orbwalker.Metadata;
    using Ensage.SDK.Service;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    [ExportOrbwalkerManager]
    public class OrbwalkerManager : ServiceManager<IOrbwalker, IOrbwalkerMetadata>, IOrbwalkerManager, IPartImportsSatisfiedNotification
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IOrbwalker active;

        [ImportingConstructor]
        public OrbwalkerManager([Import] IServiceContext context)
        {
            this.Context = context;
            this.Owner = context.Owner;
        }

        public SDKConfig.OrbwalkerConfig Config { get; private set; }

        public IServiceContext Context { get; }

        public IEnumerable<IOrbwalkingMode> CustomOrbwalkingModes => this.Modes;

        public IEnumerable<Lazy<IOrbwalkingMode, IOrbwalkingModeMetadata>> OrbwalkingModes => this.ImportedModes;

        public Vector3 OrbwalkingPoint
        {
            get
            {
                return this.Active.OrbwalkingPoint;
            }

            set
            {
                this.Active.OrbwalkingPoint = value;
            }
        }

        [ImportMany(typeof(IOrbwalker))]
        public override IEnumerable<Lazy<IOrbwalker, IOrbwalkerMetadata>> Services { get; protected set; }

        OrbwalkerSettings IOrbwalker.Settings
        {
            get
            {
                return this.Active.Settings;
            }
        }

        [ImportMany(typeof(IOrbwalkingMode))]
        private IEnumerable<Lazy<IOrbwalkingMode, IOrbwalkingModeMetadata>> ImportedModes { get; set; }

        private List<IOrbwalkingMode> Modes { get; } = new List<IOrbwalkingMode>();

        private IUpdateHandler OnUpdateHandler { get; set; }

        private Unit Owner { get; }

        public bool Attack(Unit target)
        {
            return this.active.Attack(target);
        }

        public bool CanAttack(Unit target)
        {
            return this.active.CanAttack(target);
        }

        public bool CanMove()
        {
            return this.active.CanMove();
        }

        public float GetTurnTime(Entity unit)
        {
            return this.active.GetTurnTime(unit);
        }

        public bool Move(Vector3 position)
        {
            return this.active.Move(position);
        }

        public void OnImportsSatisfied()
        {
            this.Context.Config.Settings.UpdateOrbwalkers(this.Services.Select(e => e.Metadata.Name));
        }

        public bool OrbwalkTo(Unit target)
        {
            return this.active.OrbwalkTo(target);
        }

        public void RegisterMode(IOrbwalkingMode mode)
        {
            if (this.Modes.Any(e => e == mode))
            {
                return;
            }

            Log.Info($"Register Mode {mode}");
            this.Modes.Add(mode);
            mode.Activate();
        }

        public void UnregisterMode(IOrbwalkingMode mode)
        {
            var oldMode = this.Modes.FirstOrDefault(e => e == mode);
            if (oldMode != null)
            {
                mode.Deactivate();
                this.Modes.Remove(oldMode);

                Log.Info($"Unregister Mode {mode}");
            }
        }

        protected override IOrbwalker GetSelection()
        {
            return this.Services.First(s => s.Metadata.Name == this.Context.Config.Settings.OrbwalkerSelection).Value;
        }

        protected override void OnActivate()
        {
            try
            {
                this.Config = new SDKConfig.OrbwalkerConfig(this.Context.Config.Factory);
                this.Context.Config.Settings.OrbwalkerSelection.PropertyChanged += this.OnSelectionChanged;
                this.Config.TickRate.PropertyChanged += this.OnTickRateChanged;
                this.OnImportsSatisfied();

                this.Active = this.GetSelection();

                this.OnUpdateHandler = UpdateManager.Subscribe(this.OnUpdate, this.Config.TickRate);
            }
            catch (Exception e)
            {
                Log.Warn(e);
            }
        }

        protected override void OnDeactivate()
        {
            UpdateManager.Unsubscribe(this.OnUpdateHandler);

            this.Active = null;

            this.Context.Config.Settings.OrbwalkerSelection.PropertyChanged -= this.OnSelectionChanged;
            this.Config.TickRate.PropertyChanged -= this.OnSelectionChanged;
            this.Config.Dispose();
        }

        private void OnSelectionChanged(object sender, PropertyChangedEventArgs args)
        {
            this.Active = this.GetSelection();
        }

        private void OnTickRateChanged(object sender, PropertyChangedEventArgs args)
        {
            this.OnUpdateHandler.Executor = new TimeoutHandler(this.Config.TickRate, true);
        }

        private void OnUpdate()
        {
            // no spamerino
            if (Game.IsPaused || Game.IsChatOpen || !this.Owner.IsAlive || this.Owner.IsStunned())
            {
                return;
            }

            // modes
            foreach (var mode in this.Modes.Where(e => e.CanExecute))
            {
                mode.Execute();
            }

            foreach (var mode in this.ImportedModes.Where(e => e.Value.CanExecute))
            {
                mode.Value.Execute();
            }
        }
    }
}