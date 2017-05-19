// <copyright file="Orbwalker.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Enums;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Inventory;
    using Ensage.SDK.Orbwalker.Config;
    using Ensage.SDK.Orbwalker.Metadata;
    using Ensage.SDK.Renderer.Particle;
    using Ensage.SDK.Service;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    [ExportOrbwalker("SDK")]
    public class Orbwalker : IOrbwalker
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ImportingConstructor]
        public Orbwalker(
            [Import] IServiceContext context,
            [Import] Lazy<IOrbwalkerManager> manager,
            [Import] Lazy<IParticleManager> particle,
            [Import] Lazy<IInventoryManager> inventory)
        {
            this.Context = context;
            this.Manager = manager;
            this.Particle = particle;
            this.Inventory = inventory;
            this.Owner = context.Owner;
        }

        public OrbwalkerConfig Config => this.Manager.Value.Config;

        public IServiceContext Context { get; }

        public bool IsActive { get; private set; }

        [ImportMany(typeof(IOrbwalkingMode))]
        protected IEnumerable<Lazy<IOrbwalkingMode, IOrbwalkingModeMetadata>> ImportedModes { get; set; }

        private InventoryItem EchoSabre { get; set; }

        private Lazy<IInventoryManager> Inventory { get; }

        private float LastAttackOrderIssuedTime { get; set; }

        private float LastAttackTime { get; set; }

        private float LastMoveOrderIssuedTime { get; set; }

        private Lazy<IOrbwalkerManager> Manager { get; }

        private List<IOrbwalkingMode> Modes { get; } = new List<IOrbwalkingMode>();

        private Hero Owner { get; }

        private Lazy<IParticleManager> Particle { get; }

        private float PingTime => Game.Ping / 2000f;

        private float TurnEndTime { get; set; }

        public void Activate()
        {
            if (this.IsActive)
            {
                return;
            }

            this.IsActive = true;

            Log.Debug($"Activate Orbwalker: {this.Owner.GetDisplayName()}");
            UpdateManager.Subscribe(this.OnUpdate);
            UpdateManager.Subscribe(this.OnUpdateDrawings, 1000);
            Entity.OnInt32PropertyChange += this.Hero_OnInt32PropertyChange;
            this.Inventory.Value.CollectionChanged += this.OnItemsChanged;
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

            this.TurnEndTime = this.GetTurnTime(unit);

            if (this.Owner.Attack(unit))
            {
                this.LastAttackOrderIssuedTime = time;
                return true;
            }

            return false;
        }

        public bool CanAttack(Unit target)
        {
            return this.Owner.CanAttack() && (this.GetTurnTime(target) - this.LastAttackTime) > (1f / this.Owner.AttacksPerSecond);
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

            Log.Debug($"Deactivate Orbwalker: {this.Owner.GetDisplayName()}");
            UpdateManager.Unsubscribe(this.OnUpdate);
            UpdateManager.Unsubscribe(this.OnUpdateDrawings);
            Entity.OnInt32PropertyChange -= this.Hero_OnInt32PropertyChange;
            this.Inventory.Value.CollectionChanged -= this.OnItemsChanged;

            this.Particle?.Value.Remove("AttackRange");
        }

        public float GetTurnTime(Entity unit)
        {
            return Game.RawGameTime + this.PingTime + this.Owner.TurnTime(unit.NetworkPosition) + (this.Config.Settings.TurnDelay / 1000f);
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
                return false;
            }

            if (this.Owner.Move(position))
            {
                this.LastMoveOrderIssuedTime = time;
                return true;
            }

            return false;
        }

        public bool OrbwalkTo(Unit target)
        {
            // turning
            if (this.TurnEndTime > Game.RawGameTime)
            {
                return false;
            }

            // move
            if ((target == null || !this.CanAttack(target)) && this.CanMove())
            {
                this.Move(Game.MousePosition);
                return false;
            }

            // attack
            if (target != null && this.CanAttack(target))
            {
                return this.Attack(target);
            }

            return false;
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
                    if (this.EchoSabre?.IsValid == true && Math.Abs(this.EchoSabre.Item.Cooldown) < 0.15)
                    {
                        return;
                    }

                    this.LastAttackTime = Game.RawGameTime - this.PingTime;
                    break;
            }
        }

        private void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in args.NewItems.OfType<InventoryItem>())
                {
                    if (item.Id == ItemId.item_echo_sabre)
                    {
                        this.EchoSabre = item;
                    }
                }
            }

            if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in args.OldItems.OfType<InventoryItem>())
                {
                    if (item.Id == ItemId.item_echo_sabre)
                    {
                        this.EchoSabre = null;
                    }
                }
            }
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

        private void OnUpdateDrawings()
        {
            if (this.Config.Settings.DrawRange.Value)
            {
                this.Particle?.Value.DrawRange(this.Owner, "AttackRange", this.Owner.AttackRange(this.Owner), Color.LightGreen);
            }
            else
            {
                this.Particle?.Value.Remove("AttackRange");
            }
        }
    }
}