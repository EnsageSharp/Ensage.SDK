// <copyright file="OrbwalkingTargetSelector.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Service;

    public class OrbwalkingTargetSelector
    {
        public OrbwalkingTargetSelector(IServiceContext context, TargetSelectorConfig config)
        {
            this.Context = context;
            this.Config = config;

            context.Container.BuildUp(this);
        }

        public TargetSelectorConfig Config { get; }

        public IServiceContext Context { get; }

        [Import]
        private Lazy<HealthPrediction<Creep>> HealthPrediction { get; set; }

        public IEnumerable<Building> GetBuildings()
        {
            return EntityManager<Building>.Entities.Where(b => this.Context.Owner.CanAttack(b)).OrderBy(b => b.Health);
        }

        public IEnumerable<Creep> GetCreeps()
        {
            return EntityManager<Creep>.Entities.Where(creep => this.Context.Owner.CanAttack(creep)).OrderBy(creep => creep.Health);
        }

        public IEnumerable<Creep> GetFarm()
        {
            return EntityManager<Creep>.Entities.Where(creep => this.Context.Owner.CanAttack(creep) && this.CanLasthit(creep)).OrderBy(creep => creep.Health);
        }

        public IEnumerable<Hero> GetHeroes()
        {
            return EntityManager<Hero>.Entities.Where(b => this.Context.Owner.CanAttack(b)).OrderBy(b => b.Distance2D(Game.MousePosition));
        }

        public IEnumerable<Unit> GetTargets()
        {
            IEnumerable<Unit> result = new Unit[0];

            if (this.Config.Farm.Value)
            {
                result = result.Concat(this.GetFarm().Where(e => e.IsEnemy(this.Context.Owner)));
            }

            if (this.Config.Hero.Value)
            {
                result = result.Concat(this.GetHeroes().Where(e => e.IsEnemy(this.Context.Owner)));
            }

            if (this.Config.Building.Value)
            {
                result = result.Concat(this.GetBuildings());
            }

            if (this.Config.Neutral.Value)
            {
                result = result.Concat(this.GetCreeps().Where(e => e.IsEnemy(this.Context.Owner) && e.Team == Team.Neutral));
            }

            if (this.Config.Deny.Value)
            {
                result = result.Concat(this.GetCreeps().Where(e => e.IsAlly(this.Context.Owner)));
            }

            if (this.Config.Creep.Value)
            {
                result = result.Concat(this.GetCreeps());
            }

            return result;
        }

        private bool CanLasthit(Unit target)
        {
            var owner = this.Context.Owner;
            var arrivalTime = owner.GetAutoAttackArrivalTime(target) + (Game.Ping / 2000f);
            var dmg = owner.GetAttackDamage(target, true);

            return dmg > this.HealthPrediction.Value.GetPredictedHealth(target, arrivalTime);
        }
    }
}