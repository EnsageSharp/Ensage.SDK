// <copyright file="AutoAttackModeSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Modes
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Extensions;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Orbwalker.Config;
    using Ensage.SDK.Prediction;

    using log4net;

    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Logging;

    internal class AutoAttackModeSelector
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IHealthPrediction healthPrediction;

        public AutoAttackModeSelector(Unit owner, ITargetSelectorManager manager, AutoAttackModeConfig config)
        {
            this.Owner = owner;
            this.Manager = manager;
            this.Config = config;
        }

        protected float BonusRange
        {
            get
            {
                return this.Owner.IsMelee ? this.Config.BonusMeleeRange : this.Config.BonusRangedRange;
            }
        }

        protected IHealthPrediction HealthPrediction
        {
            get
            {
                if (this.healthPrediction == null)
                {
                    this.healthPrediction = IoC.Get<IHealthPrediction>();
                }

                return this.healthPrediction;
            }
        }

        protected ITargetSelectorManager Manager { get; }

        private static bool LaneClearRateLimitResult { get; set; }

        private static double LaneClearRateLimitTime { get; set; }

        private AutoAttackModeConfig Config { get; }

        private Unit Owner { get; }

        public Unit GetTarget()
        {
            if (this.Config.Farm)
            {
                var killableCreep = EntityManager<Creep>.Entities.FirstOrDefault(unit => unit.IsVisible && this.IsValid(unit) && this.CanKill(unit));

                if (killableCreep != null)
                {
                    return killableCreep;
                }
            }

            if (this.Config.Farm)
            {
                if ((Game.RawGameTime - LaneClearRateLimitTime) > 0.25f)
                {
                    LaneClearRateLimitResult = this.HealthPrediction.ShouldWait();
                    LaneClearRateLimitTime = Game.RawGameTime;
                }

                if (LaneClearRateLimitResult)
                {
                    return null;
                }
            }

            if (this.Config.Deny)
            {
                var denyCreep = EntityManager<Creep>.Entities.FirstOrDefault(
                    unit => unit.IsVisible && this.IsValid(unit, true) && unit.HealthPercent() < 0.5f && this.CanKill(unit));

                if (denyCreep != null)
                {
                    return denyCreep;
                }
            }

            if (this.Config.Hero)
            {
                var hero = this.Manager.Active.GetTargets()?.FirstOrDefault(unit => unit.IsVisible && this.IsValid(unit));

                if (hero != null)
                {
                    return hero;
                }
            }

            if (this.Config.Building)
            {
                var barracks = EntityManager<Building>.Entities.FirstOrDefault(unit => unit.IsVisible && this.IsValid(unit));
                if (barracks != null)
                {
                    return barracks;
                }

                var tower = EntityManager<Tower>.Entities.FirstOrDefault(unit => unit.IsVisible && this.IsValid(unit));
                if (tower != null)
                {
                    return tower;
                }
            }

            if (this.Config.Neutral)
            {
                var neutral = EntityManager<Creep>.Entities.FirstOrDefault(unit => unit.IsVisible && unit.IsNeutral && this.IsValid(unit));

                if (neutral != null)
                {
                    return neutral;
                }
            }

            if (this.Config.Creep)
            {
                var neutral = EntityManager<Creep>.Entities.FirstOrDefault(unit => unit.IsVisible && this.IsValid(unit));

                if (neutral != null)
                {
                    return neutral;
                }
            }

            return null;
        }

        private bool CanKill(Unit target)
        {
            var extraMoveTime = 0.0f;
            if (this.BonusRange > 0.0f && !this.Owner.IsInAttackRange(target))
            {
                var speed = this.Owner.MovementSpeed;
                if (speed > 0)
                {
                    var distance = Math.Max(0.0f, this.Owner.Distance2D(target) - this.Owner.GetAttackRange());
                    extraMoveTime = (distance / speed) + 0.1f;
                }
            }

            return this.Owner.GetAttackDamage(target, true)
                   > this.HealthPrediction.GetPrediction(target, (this.Owner.GetAutoAttackArrivalTime(target) + (Game.Ping / 2000f) + extraMoveTime) - 0.15f);
        }

        private bool IsValid(Unit target, bool myTeam = false)
        {
            if (myTeam)
            {
                return target.Team == this.Owner.Team && this.Owner.IsValidOrbwalkingTarget(target, this.BonusRange);
            }

            return target.Team != this.Owner.Team && this.Owner.IsValidOrbwalkingTarget(target, this.BonusRange);
        }
    }
}