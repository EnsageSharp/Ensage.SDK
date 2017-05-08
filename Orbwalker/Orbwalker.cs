// <copyright file="Orbwalker.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Orbwalker.Config;
    using Ensage.SDK.Orbwalker.Metadata;
    using Ensage.SDK.Renderer.Particle;
    using Ensage.SDK.Service;

    using log4net;

    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    [ExportOrbwalker("SDK")]
    public class Orbwalker : IOrbwalker
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ImportingConstructor]
        public Orbwalker([Import] IServiceContext context, [Import] Lazy<IOrbwalkerManager> manager)
        {
            this.Context = context;
            this.Manager = manager;
            this.Owner = context.Owner;
        }

        public OrbwalkerConfig Config => this.Manager.Value.Config;

        public IServiceContext Context { get; }

        public bool IsActive { get; private set; }

        public float TurnEndTime { get; private set; }

        [ImportMany(typeof(IOrbwalkingMode))]
        protected IEnumerable<Lazy<IOrbwalkingMode, IOrbwalkingModeMetadata>> DefaultModes { get; set; }

        [Import(typeof(IParticleManager))]
        protected Lazy<IParticleManager> ParticleManager { get; set; }

        private float LastAttackOrderIssuedTime { get; set; }

        private float LastAttackTime { get; set; }

        private float LastMoveOrderIssuedTime { get; set; }

        private Lazy<IOrbwalkerManager> Manager { get; }

        private List<IOrbwalkingMode> Modes { get; } = new List<IOrbwalkingMode>();

        private Hero Owner { get; }

        private float PingTime => Game.Ping / 2000f;

        public void Activate()
        {
            if (this.IsActive)
            {
                return;
            }

            this.IsActive = true;

            Log.Debug($"Activate Orbwalker:{this.Owner.GetDisplayName()}");
            UpdateManager.Subscribe(this.OnUpdate);
            UpdateManager.Subscribe(this.OnUpdateDrawings, 1000);
            Entity.OnInt32PropertyChange += this.Hero_OnInt32PropertyChange;
        }

        public bool Attack(Unit unit)
        {
            if (!this.Config.Settings.Attack)
            {
                return false;
            }

            var time = Game.RawGameTime;
            if ((time - this.LastAttackOrderIssuedTime) < (this.Config.Settings.AttackDelay / 1000f))
            {
                return false;
            }

            this.TurnEndTime = Game.RawGameTime + this.PingTime + (float)this.Owner.TurnTime(unit.NetworkPosition) + 0.1f;
            this.Owner.Attack(unit);
            return true;
        }

        public bool CanAttack(Unit target)
        {
            var rotationTime = this.Owner.TurnTime(target.NetworkPosition);
            return this.Owner.CanAttack() && ((Game.RawGameTime + 0.1f + rotationTime + this.PingTime) - this.LastAttackTime) > (1f / this.Owner.AttacksPerSecond);
        }

        public bool CanMove()
        {
            return (((Game.RawGameTime - 0.1f) + this.PingTime) - this.LastAttackTime) > this.Owner.AttackPoint();
        }

        public void Deactivate()
        {
            if (!this.IsActive)
            {
                return;
            }

            this.IsActive = false;

            Log.Debug($"Deactivate Orbwalker:{this.Owner.GetDisplayName()}");
            UpdateManager.Unsubscribe(this.OnUpdate);
            UpdateManager.Unsubscribe(this.OnUpdateDrawings);
            Entity.OnInt32PropertyChange -= this.Hero_OnInt32PropertyChange;

            this.ParticleManager?.Value.Remove("AttackRange");
        }

        public bool Move(Vector3 position)
        {
            if (!this.Config.Settings.Move)
            {
                return false;
            }

            var time = Game.RawGameTime;
            if ((time - this.LastMoveOrderIssuedTime) < (this.Config.Settings.MoveDelay / 1000f))
            {
                // 0.005f
                return false;
            }

            if (this.Owner.Move(position))
            {
                this.LastMoveOrderIssuedTime = Game.RawGameTime;
                return true;
            }

            return false;
        }

        public void RegisterMode(IOrbwalkingMode mode)
        {
            if (this.Modes.All(e => e != mode))
            {
                return;
            }

            this.Modes.Add(mode);

            var contoller = mode as IControllable;
            if (contoller != null && !contoller.IsActive)
            {
                contoller.Activate();
            }
        }

        public void UnregisterMode(IOrbwalkingMode mode)
        {
            var oldMode = this.Modes.FirstOrDefault(e => e == mode);
            if (oldMode != null)
            {
                this.Modes.Remove(oldMode);

                var contoller = oldMode as IControllable;
                if (contoller != null && !contoller.IsActive)
                {
                    contoller.Activate();
                }
            }
        }

        private void Hero_OnInt32PropertyChange(Entity sender, Int32PropertyChangeEventArgs args)
        {
            if (sender != this.Owner)
            {
                return;
            }

            if (!args.PropertyName.Equals("m_networkactivity", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            var newNetworkActivity = (NetworkActivity)args.NewValue;

            switch (newNetworkActivity)
            {
                case NetworkActivity.Attack:
                case NetworkActivity.Attack2:
                case NetworkActivity.AttackEvent:
                    // var diff = Game.RawGameTime - this.LastAttackTime;
                    this.LastAttackTime = Game.RawGameTime - (Game.Ping / 2000f);
                    break;
            }
        }

        private void OnUpdate()
        {
            // no spamerino
            if (Game.IsPaused || Game.IsChatOpen || !this.Owner.IsAlive || this.Owner.IsStunned())
            {
                return;
            }

            if (this.DefaultModes == null)
            {
                return;
            }

            // modes
            foreach (var mode in this.Modes.Where(e => e.CanExecute))
            {
                mode.Execute();
            }

            foreach (var mode in this.DefaultModes.Where(e => e.Value.CanExecute))
            {
                mode.Value.Execute();
            }
        }

        private void OnUpdateDrawings()
        {
            if (this.Config.Settings.DrawRange.Value)
            {
                this.ParticleManager?.Value.DrawRange(this.Owner, "AttackRange", this.Owner.AttackRange(this.Owner), Color.LightGreen);
            }
            else
            {
                this.ParticleManager?.Value.Remove("AttackRange");
            }
        }
    }
}