// <copyright file="Orbwalker.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Inventory;
    using Ensage.SDK.Orbwalker.Config;
    using Ensage.SDK.Orbwalker.Metadata;
    using Ensage.SDK.Service;

    using log4net;

    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    [ExportOrbwalker("SDK")]
    public sealed class Orbwalker : IOrbwalker
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly HashSet<NetworkActivity> attackActivities = new HashSet<NetworkActivity>
        {
            NetworkActivity.Attack,
            NetworkActivity.Attack2,
            NetworkActivity.AttackEvent
        };

        private readonly HashSet<NetworkActivity> attackCancelActivities = new HashSet<NetworkActivity>
        {
            NetworkActivity.Idle,
            NetworkActivity.IdleRare,
            NetworkActivity.Move
        };

        [ImportingConstructor]
        public Orbwalker([Import] IServiceContext context)
        {
            this.Context = context;
            this.Owner = context.Owner;
        }

        public IServiceContext Context { get; }

        public bool IsActive { get; private set; }

        public Vector3 OrbwalkingPoint { get; set; } = Vector3.Zero;

        public OrbwalkerSettings Settings { get; private set; }

        private InventoryItem EchoSabre { get; set; }

        private float LastAttackOrderIssuedTime { get; set; }

        private float LastAttackTime { get; set; }

        private float LastMoveOrderIssuedTime { get; set; }

        private Unit Owner { get; }

        private float PingTime
        {
            get
            {
                return Game.Ping / 2000f;
            }
        }

        private float TurnEndTime { get; set; }

        public void Activate()
        {
            if (this.IsActive)
            {
                return;
            }

            this.IsActive = true;

            this.Settings = new OrbwalkerSettings(this.Context.Orbwalker.Config.Factory, this.Owner);
            this.Settings.DrawRange.PropertyChanged += this.OnDrawRangeChanged;
            this.Settings.DrawHoldRange.PropertyChanged += this.OnDrawHoldRangeChanged;

            if (this.Settings.DrawRange || this.Settings.DrawHoldRange)
            {
                UpdateManager.Subscribe(this.OnUpdateDrawings, 1000);
            }

            this.Context.Inventory.CollectionChanged += this.OnItemsChanged;

            var hero = this.Owner as Hero;
            if (hero?.HeroId == HeroId.npc_dota_hero_visage)
            {
                //HACK visage workaround
                Player.OnExecuteOrder += this.OnExecuteOrder;
            }
            else
            {
                if (hero?.HeroId == HeroId.npc_dota_hero_viper)
                {
                    //HACK viper Q workaround
                    this.attackActivities.Add(NetworkActivity.CastAbilityQ);
                }

                Entity.OnInt32PropertyChange += this.OnNetworkActivity;
            }
        }

        public bool Attack(Unit unit, float time)
        {
            if (!this.Settings.Attack)
            {
                return false;
            }

            if ((time - this.LastAttackOrderIssuedTime) < (this.Settings.AttackDelay / 1000f))
            {
                return false;
            }

            this.TurnEndTime = this.GetTurnTime(unit, time);

            if (this.Owner.Attack(unit))
            {
                this.LastAttackOrderIssuedTime = time;
                return true;
            }

            return false;
        }

        public bool Attack(Unit unit)
        {
            return this.Attack(unit, Game.RawGameTime);
        }

        public bool CanAttack(Unit target, float time)
        {
            return this.Owner.CanAttack() && (this.GetTurnTime(target, time) - this.LastAttackTime) > (1f / this.Owner.AttacksPerSecond);
        }

        public bool CanAttack(Unit target)
        {
            return this.CanAttack(target, Game.RawGameTime);
        }

        public bool CanMove(float time)
        {
            return (((time - 0.1f) + this.PingTime) - this.LastAttackTime) > this.Owner.AttackPoint();
        }

        public bool CanMove()
        {
            return this.CanMove(Game.RawGameTime);
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
            Player.OnExecuteOrder -= this.OnExecuteOrder;
            this.attackActivities.Remove(NetworkActivity.CastAbilityQ);
            this.Context.Inventory.CollectionChanged -= this.OnItemsChanged;
            this.Context.Particle.Remove("AttackRange");
            this.Settings.DrawRange.PropertyChanged -= this.OnDrawRangeChanged;
            this.Settings.DrawHoldRange.PropertyChanged -= this.OnDrawHoldRangeChanged;
            this.Settings.Dispose();
        }

        public float GetTurnTime(Entity unit, float time)
        {
            return time + this.PingTime + this.Owner.TurnTime(unit.NetworkPosition) + (this.Settings.TurnDelay / 1000f);
        }

        public float GetTurnTime(Entity unit)
        {
            return this.GetTurnTime(unit, Game.RawGameTime);
        }

        public bool Move(Vector3 position, float time)
        {
            if (!this.Settings.Move)
            {
                return false;
            }

            if (this.Owner.Position.Distance(position) < this.Settings.HoldRange)
            {
                return false;
            }

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

        public bool Move(Vector3 position)
        {
            return this.Move(position, Game.RawGameTime);
        }

        public bool OrbwalkTo([CanBeNull] Unit target)
        {
            var time = Game.RawGameTime;

            // turning
            if (this.TurnEndTime > time)
            {
                return false;
            }

            // owner disabled
            if (this.Owner.IsChanneling() || !this.Owner.IsAlive || this.Owner.IsStunned())
            {
                return false;
            }

            var validTarget = target != null;

            // move
            if ((!validTarget || !this.CanAttack(target)) && this.CanMove(time))
            {
                if (this.OrbwalkingPoint != Vector3.Zero)
                {
                    return this.Move(this.OrbwalkingPoint, time);
                }

                return this.Move(Game.MousePosition, time);
            }

            // attack
            if (validTarget && this.CanAttack(target))
            {
                return this.Attack(target, time);
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
                this.OnUpdateDrawings();
            }
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            if (args.OrderId != OrderId.AttackTarget || args.IsQueued || !args.Process || !args.Entities.Contains(this.Owner))
            {
                return;
            }

            var target = args.Target as Unit;
            if (target == null || !target.IsValid)
            {
                return;
            }

            if (this.CanMove())
            {
                this.LastAttackTime = this.GetTurnTime(target) - this.PingTime;
            }
        }

        private void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in args.NewItems.OfType<InventoryItem>())
                {
                    if (item.Id == AbilityId.item_echo_sabre)
                    {
                        this.EchoSabre = item;
                    }
                }
            }

            if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in args.OldItems.OfType<InventoryItem>())
                {
                    if (item.Id == AbilityId.item_echo_sabre)
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

            if (this.attackActivities.Contains(newNetworkActivity))
            {
                if (this.EchoSabre?.IsValid == true && Math.Abs(this.EchoSabre.Item.Cooldown) < 0.15)
                {
                    return;
                }

                this.LastAttackTime = Game.RawGameTime - this.PingTime;
            }
            else if (this.attackCancelActivities.Contains(newNetworkActivity))
            {
                if (!this.CanMove(Game.RawGameTime + 0.05f))
                {
                    this.LastAttackTime = 0;
                }
            }
        }

        private void OnUpdateDrawings()
        {
            if (!this.Owner.IsValid)
            {
                return;
            }

            if (this.Settings.DrawRange)
            {
                this.Context.Particle.DrawRange(this.Owner, "AttackRange", this.Owner.AttackRange(this.Owner), Color.LightGreen);
            }
            else
            {
                this.Context.Particle.Remove("AttackRange");
            }

            if (this.Settings.DrawHoldRange)
            {
                this.Context.Particle.DrawRange(this.Owner, "HoldRange", this.Settings.HoldRange, Color.DarkViolet);
            }
            else
            {
                this.Context.Particle.Remove("HoldRange");
            }
        }
    }
}