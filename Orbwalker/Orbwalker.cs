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

    using Ensage.Common.Menu;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Orbwalker.Metadata;
    using Ensage.SDK.Prediction;
    using Ensage.SDK.Prediction.Metadata;
    using Ensage.SDK.Renderer.Particle;
    using Ensage.SDK.Service;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    [ExportOrbwalker("SDK")]
    public class Orbwalker : IOrbwalker
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool isActive;

        [ImportingConstructor]
        public Orbwalker([Import] IServiceContext context)
        {
            Log.Debug($"Create Orbwalker:{context.Owner.GetDisplayName()}");
            this.Context = context;
            this.Owner = context.Owner;
            this.Config = new OrbwalkerConfig(context);
            this.Config.Active.Item.ValueChanged += this.OnActiveValueChanged;

            if (this.Config.Active.Value)
            {
                this.Activate();
            }
        }

        public OrbwalkerConfig Config { get; }

        public IServiceContext Context { get; }

        public float TurnEndTime { get; private set; }

        [ImportHealthPrediction]
        protected Lazy<IHealthPrediction> HealthPrediction { get; set; }

        [ImportMany(typeof(IOrbwalkingMode))]
        protected IEnumerable<Lazy<IOrbwalkingMode, IOrbwalkingModeMetadata>> Modes { get; set; }

        [Import(typeof(IParticleManager))]
        protected Lazy<IParticleManager> ParticleManager { get; set; }

        private float LastAttackOrderIssuedTime { get; set; }

        private float LastAttackTime { get; set; }

        private float LastMoveOrderIssuedTime { get; set; }

        private Unit Owner { get; }

        private float PingTime => Game.Ping / 2000f;

        public void Activate()
        {
            if (this.isActive)
            {
                return;
            }

            this.isActive = true;

            UpdateManager.Subscribe(this.OnUpdate);
            UpdateManager.Subscribe(this.OnUpdateDrawings, 1000);
            Entity.OnInt32PropertyChange += this.Hero_OnInt32PropertyChange;
        }

        public bool Attack(Unit unit)
        {
            if (!this.Config.Settings.Attack.Value)
            {
                return false;
            }

            var time = Game.RawGameTime;
            if ((time - this.LastAttackOrderIssuedTime) < (this.Config.Settings.AttackDelay.Value.Value / 1000f))
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
            if (!this.isActive)
            {
                return;
            }

            this.isActive = false;

            UpdateManager.Unsubscribe(this.OnUpdate);
            UpdateManager.Unsubscribe(this.OnUpdateDrawings);
            Entity.OnInt32PropertyChange -= this.Hero_OnInt32PropertyChange;
        }

        public bool Move(Vector3 position)
        {
            if (!this.Config.Settings.Move.Value)
            {
                return false;
            }

            var time = Game.RawGameTime;
            if ((time - this.LastMoveOrderIssuedTime) < (this.Config.Settings.MoveDelay.Value.Value / 1000f))
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

        private void OnActiveValueChanged(object sender, OnValueChangeEventArgs args)
        {
            if (args.GetNewValue<bool>())
            {
                this.Activate();
            }
            else
            {
                this.Deactivate();
            }
        }

        private void OnUpdate()
        {
            // no spamerino
            if (Game.IsPaused || Game.IsChatOpen || !this.Owner.IsAlive || this.Owner.IsStunned())
            {
                return;
            }

            if (this.Modes == null)
            {
                return;
            }

            // modes
            foreach (var mode in this.Modes.Where(e => e.Value.CanExecute))
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