// <copyright file="Orbwalker.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Orbwalker
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Ensage.Common.Menu;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    public class Orbwalker
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public OrbwalkingMode Mode = OrbwalkingMode.None;

        private readonly HashSet<NetworkActivity> attackActivityList =
            new HashSet<NetworkActivity>
            {
                NetworkActivity.Attack,
                NetworkActivity.Attack2,
                NetworkActivity.AttackEvent
            };

        public Orbwalker(Unit owner)
        {
            this.Owner = owner;
        }

        public enum OrbwalkingMode
        {
            Combo,

            LaneClear,

            LastHit,

            Deny,

            Mixed,

            None
        }

        public MenuItem Clear { get; set; }

        public MenuItem Combo { get; set; }

        public MenuItem Deny { get; set; }

        public MenuItem Farm { get; set; }

        public bool LaneClearRateLimitResult { get; set; }

        public float LaneClearRateLimitTime { get; set; }

        public float LastAttackOrderIssuedTime { get; set; }

        public float LastAttackTime { get; set; }

        public float LastMoveOrderIssuedTime { get; set; }

        public MenuItem Mixed { get; set; }

        public Unit Owner { get; set; }

        public float TurnEndTime { get; set; }

        public bool Attack(Unit unit)
        {
            var time = Game.RawGameTime;
            if ((time - this.LastAttackOrderIssuedTime) < 0.005f)
            {
                return false;
            }

            this.TurnEndTime = Game.RawGameTime + (Game.Ping / 2000f) + (float)this.Owner.TurnTime(unit.NetworkPosition) + 0.1f;
            this.Owner.Attack(unit);
            return true;
        }

        public bool CanAttack(Unit target)
        {
            var rotationTime = this.Owner.TurnTime(target.NetworkPosition);
            return this.Owner.CanAttack() && ((Game.RawGameTime + 0.1f + rotationTime + (Game.Ping / 2000f)) - this.LastAttackTime) > (1f / this.Owner.AttacksPerSecond);
        }

        public bool CanMove()
        {
            return (((Game.RawGameTime - 0.1f) + (Game.Ping / 2000f)) - this.LastAttackTime) > this.Owner.AttackPoint();
        }

        public bool Load()
        {
            Game.OnIngameUpdate += this.OnUpdate;
            Entity.OnInt32PropertyChange += this.Hero_OnInt32PropertyChange;
            return true;
        }

        public bool Move(Vector3 position)
        {
            var time = Game.RawGameTime;
            if ((time - this.LastMoveOrderIssuedTime) < 0.005f)
            {
                return false;
            }

            this.LastMoveOrderIssuedTime = Game.RawGameTime;
            this.Owner.Move(position);

            return true;
        }

        public bool Unload()
        {
            Game.OnIngameUpdate -= this.OnUpdate;
            Entity.OnInt32PropertyChange -= this.Hero_OnInt32PropertyChange;

            return true;
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

            if (this.attackActivityList.Contains(newNetworkActivity))
            {
                var diff = Game.RawGameTime - this.LastAttackTime;
                this.LastAttackTime = Game.RawGameTime - (Game.Ping / 2000f);
            }
        }

        private void OnUpdate(EventArgs args)
        {
            try
            {
                this.Mode = OrbwalkingMode.None;

                // no spamerino
                if (Game.IsPaused || Game.IsChatOpen)
                {
                    return;
                }

                // TODO: TARGET
                Unit target = null;

                // TODO: MODE
                if (this.Mode == OrbwalkingMode.None)
                {
                    return;
                }

                // turning
                if (this.TurnEndTime > Game.RawGameTime)
                {
                    return;
                }

                if ((target == null || !this.CanAttack(target)) && this.CanMove())
                {
                    this.Move(Game.MousePosition);
                    return;
                }

                if (target != null && this.CanAttack(target))
                {
                    this.Attack(target);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private bool ShouldWait()
        {
            var time = 2f / this.Owner.AttacksPerSecond;

            foreach (var unit in EntityManager<Creep>.Entities)
            {
                if (unit.Team != this.Owner.Team && this.Owner.IsValidOrbwalkingTarget(unit) &&
                    HealthPredictionOLD.Instance().GetPredictedHealth(unit, time) < this.Owner.GetAttackDamage(unit, true))
                {
                    return true;
                }
            }

            return false;
        }
    }
}