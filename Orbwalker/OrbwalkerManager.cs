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
    public class OrbwalkerManager : ControllableService, IOrbwalkerManager
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IOrbwalker active;

        [ImportingConstructor]
        public OrbwalkerManager([Import] IServiceContext context)
        {
            this.Context = context;
            this.Owner = context.Owner;
        }

        public IOrbwalker Active
        {
            get
            {
                return this.active;
            }

            set
            {
                if (value == null && this.active != null)
                {
                    Log.Debug($"Deactivate Orbwalker {this.active}");
                    this.active.Deactivate();
                    this.active = null;
                    return;
                }

                if (EqualityComparer<IOrbwalker>.Default.Equals(this.active, value))
                {
                    return;
                }

                Log.Debug($"Activate Orbwalker {value}");
                this.active?.Deactivate();
                this.active = value;
                this.active?.Activate();
            }
        }

        public OrbwalkerConfig Config { get; private set; }

        public IServiceContext Context { get; }

        public IEnumerable<IOrbwalkingMode> CustomOrbwalkingModes => this.Modes;

        [ImportMany(typeof(IOrbwalker))]
        public IEnumerable<Lazy<IOrbwalker, IOrbwalkerMetadata>> Orbwalkers { get; private set; }

        public IEnumerable<Lazy<IOrbwalkingMode, IOrbwalkingModeMetadata>> OrbwalkingModes => this.ImportedModes;

        IServiceContext IOrbwalker.Context
        {
            get
            {
                return this.active.Context;
            }
        }

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

        protected override void OnActivate()
        {
            try
            {
                this.Config = new OrbwalkerConfig(this.Context.Owner.GetDisplayName(), this.Orbwalkers.Select(e => e.Metadata.Name).ToArray());
                this.Config.Selection.PropertyChanged += this.OnSelectionChanged;
                this.Config.TickRate.PropertyChanged += this.OnTickRateChanged;

                // activate selection
                this.Active = this.Orbwalkers.FirstOrDefault(e => e.Metadata.Name == this.Config.Selection)?.Value;

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

            this.Active?.Deactivate();
            this.Active = null;

            this.Config.Selection.PropertyChanged -= this.OnSelectionChanged;
            this.Config.TickRate.PropertyChanged -= this.OnSelectionChanged;
            this.Config?.Dispose();
        }

        private void OnSelectionChanged(object sender, PropertyChangedEventArgs args)
        {
            // update selection
            string name = this.Config.Selection;
            this.Active = this.Orbwalkers.FirstOrDefault(e => e.Metadata.Name == name)?.Value;
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