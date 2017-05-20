// <copyright file="Orbwalker.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Enums;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Inventory;
    using Ensage.SDK.Menu;
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
            [Import] Lazy<IParticleManager> particle,
            [Import] Lazy<IInventoryManager> inventory)
        {
            this.Context = context;
            this.Particle = particle;
            this.Inventory = inventory;
            this.Owner = context.Owner;
        }

        public IServiceContext Context { get; }

        public bool IsActive { get; private set; }

        public OrbwalkerSettings Settings { get; private set; }

        private InventoryItem EchoSabre { get; set; }

        private Lazy<IInventoryManager> Inventory { get; }

        private float LastAttackOrderIssuedTime { get; set; }

        private float LastAttackTime { get; set; }

        private float LastMoveOrderIssuedTime { get; set; }

        private Unit Owner { get; }

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

            this.Settings = new OrbwalkerSettings(MenuFactory.Attach("Orbwalker"), this.Owner);
            this.Settings.DrawRange.PropertyChanged += this.OnDrawRangeChanged;
            this.Settings.DrawHoldRange.PropertyChanged += this.OnDrawHoldRangeChanged;

            if (this.Settings.DrawRange || this.Settings.DrawHoldRange)
            {
                UpdateManager.Subscribe(this.OnUpdateDrawings, 1000);
            }

            Entity.OnInt32PropertyChange += this.OnNetworkActivity;
            this.Inventory.Value.CollectionChanged += this.OnItemsChanged;
        }

        public bool Attack(Unit unit)
        {
            if (!this.Settings.Attack)
            {
                return false;
            }

            var time = Game.RawGameTime;
            if ((time - this.LastAttackOrderIssuedTime) < (this.Settings.AttackDelay / 1000f))
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
            UpdateManager.Unsubscribe(this.OnUpdateDrawings);
            Entity.OnInt32PropertyChange -= this.OnNetworkActivity;
            this.Inventory.Value.CollectionChanged -= this.OnItemsChanged;
            this.Particle?.Value.Remove("AttackRange");
            this.Settings.DrawRange.PropertyChanged -= this.OnDrawRangeChanged;
            this.Settings.DrawHoldRange.PropertyChanged -= this.OnDrawHoldRangeChanged;
            this.Settings.Dispose();
        }

        public float GetTurnTime(Entity unit)
        {
            return Game.RawGameTime + this.PingTime + this.Owner.TurnTime(unit.NetworkPosition) + (this.Settings.TurnDelay / 1000f);
        }

        public bool Move(Vector3 position)
        {
            if (!this.Settings.Move)
            {
                return false;
            }

            if (this.Owner.Position.Distance(position) < this.Settings.HoldRange)
            {
                return false;
            }

            var time = Game.RawGameTime;
            if ((time - this.LastMoveOrderIssuedTime) < (this.Settings.MoveDelay / 1000f))
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

        private void OnDrawHoldRangeChanged(object sender, PropertyChangedEventArgs e)
        {
            this.OnUpdateDrawings();
        }

        private void OnDrawRangeChanged(object sender, PropertyChangedEventArgs args)
        {
            if (this.Settings.DrawRange)
            {
                UpdateManager.Subscribe(this.OnUpdateDrawings, 1000);
            }
            else
            {
                UpdateManager.Unsubscribe(this.OnUpdateDrawings);
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

        private void OnNetworkActivity(Entity sender, Int32PropertyChangeEventArgs args)
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

        private void OnUpdateDrawings()
        {
            if (this.Settings.DrawRange)
            {
                this.Particle?.Value.DrawRange(this.Owner, "AttackRange", this.Owner.AttackRange(this.Owner), Color.LightGreen);
            }
            else
            {
                this.Particle?.Value.Remove("AttackRange");
            }

            if (this.Settings.DrawHoldRange)
            {
                this.Particle?.Value.DrawRange(this.Owner, "HoldRange", this.Settings.HoldRange, Color.DarkViolet);
            }
            else
            {
                this.Particle?.Value.Remove("HoldRange");
            }
        }
    }
}