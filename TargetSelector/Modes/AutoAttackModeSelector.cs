// <copyright file="AutoAttackModeSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector.Modes
{
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Orbwalker;
    using Ensage.SDK.Prediction;

    using log4net;

    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Logging;

    internal class AutoAttackModeSelector
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IHealthPrediction healthPrediction;

        private ITargetSelectorManager manager;

        public AutoAttackModeSelector(Hero owner, AutoAttackModeConfig config)
        {
            this.Owner = owner;
            this.Config = config;
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

        protected ITargetSelectorManager Manager
        {
            get
            {
                if (this.manager == null)
                {
                    this.manager = IoC.Get<ITargetSelectorManager>();
                }

                return this.manager;
            }
        }

        private static bool LaneClearRateLimitResult { get; set; }

        private static double LaneClearRateLimitTime { get; set; }

        private AutoAttackModeConfig Config { get; }

        private Hero Owner { get; }

        public Unit GetTarget()
        {
            if (this.Config.Farm.Value)
            {
                var killableCreep = EntityManager<Creep>.Entities.FirstOrDefault(unit => this.IsValid(unit) && this.CanKill(unit));

                if (killableCreep != null)
                {
                    return killableCreep;
                }
            }

            if (this.Config.Farm.Value)
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

            if (this.Config.Deny.Value)
            {
                var denyCreep = EntityManager<Creep>.Entities.FirstOrDefault(unit => this.IsValid(unit, true) && unit.HealthPercent() < 0.5f && this.CanKill(unit));

                if (denyCreep != null)
                {
                    return denyCreep;
                }
            }

            if (this.Config.Hero.Value)
            {
                var hero = this.Manager.Active.GetTargets()?.FirstOrDefault(unit => this.IsValid(unit));

                if (hero != null)
                {
                    return hero;
                }
            }

            if (this.Config.Building.Value)
            {
                var barracks = EntityManager<Building>.Entities.FirstOrDefault(unit => this.IsValid(unit));
                if (barracks != null)
                {
                    return barracks;
                }

                var tower = EntityManager<Tower>.Entities.FirstOrDefault(unit => this.IsValid(unit));
                if (tower != null)
                {
                    return tower;
                }
            }

            if (this.Config.Neutral.Value)
            {
                var neutral = EntityManager<Creep>.Entities.FirstOrDefault(unit => unit.IsNeutral && this.IsValid(unit));

                if (neutral != null)
                {
                    return neutral;
                }
            }

            if (this.Config.Creep.Value)
            {
                var neutral = EntityManager<Creep>.Entities.FirstOrDefault(unit => this.IsValid(unit));

                if (neutral != null)
                {
                    return neutral;
                }
            }

            return null;
        }

        private bool CanKill(Unit target)
        {
            return this.Owner.GetAttackDamage(target, true) > this.HealthPrediction.GetPrediction(target, this.Owner.GetAutoAttackArrivalTime(target) + (Game.Ping / 2000f));
        }

        private bool IsValid(Unit target, bool myTeam = false)
        {
            if (myTeam)
            {
                return target.Team == this.Owner.Team && this.Owner.IsValidOrbwalkingTarget(target);
            }

            return target.Team != this.Owner.Team && this.Owner.IsValidOrbwalkingTarget(target);
        }
    }
}