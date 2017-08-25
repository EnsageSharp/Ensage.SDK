// <copyright file="HealthPrediction.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Prediction.Metadata;
    using Ensage.SDK.Service;

    [ExportHealthPrediction]
    public sealed class HealthPrediction : ControllableService, IHealthPrediction, IDisposable
    {
        private readonly IServiceContext context;

        private bool disposed;

        [ImportingConstructor]
        public HealthPrediction([Import] IServiceContext context)
        {
            this.context = context;
            this.Owner = this.context.Owner;
        }

        private Dictionary<uint, CreepStatus> CreepStatuses { get; } = new Dictionary<uint, CreepStatus>();

        private Unit Owner { get; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public float GetPrediction(Unit unit, float untilTime)
        {
            var now = Game.RawGameTime;
            var health = (float)unit.Health;
            untilTime = Math.Max(0f, untilTime);
            untilTime = now + untilTime;

            var handle = unit.Handle;
            var team = unit.Team;

            foreach (var pair in this.CreepStatuses.Where(e => e.Value.IsValid && e.Value.Team != team && e.Value.Target?.Handle == handle))
            {
                var entry = pair.Value;
                var damage = entry.Source.GetAttackDamage(unit, true);

                float attackHitTime;

                if (entry.LastAttackAnimationTime == 0f || (now - entry.LastAttackAnimationTime) > (entry.TimeBetweenAttacks + 0.2))
                {
                    continue;
                }

                if (entry.IsMelee)
                {
                    // melee creeps
                    attackHitTime = entry.LastAttackAnimationTime + entry.AttackPoint;
                }
                else
                {
                    // ranged creeps
                    attackHitTime = (entry.LastAttackAnimationTime - entry.TimeBetweenAttacks) + entry.GetAutoAttackArrivalTime(unit);
                }

                // delete next line and improve prediction if Stop() will be used to cancel auto attack :broscience:
                attackHitTime -= 0.05f;

                while (attackHitTime <= untilTime)
                {
                    if (attackHitTime > now)
                    {
                        health -= damage;
                    }

                    attackHitTime += entry.TimeBetweenAttacks;
                }
            }

            if (health > 0f)
            {
                // towers
                var closestTower = EntityManager<Tower>.Entities.OrderBy(tower => tower.Distance2D(this.Owner)).FirstOrDefault();
                if (closestTower != null)
                {
                    var towerTarget = closestTower.AttackTarget;
                    if (towerTarget != null && towerTarget == unit)
                    {
                        var creepStatus = this.GetCreepStatusEntry(closestTower);
                        var damage = closestTower.GetAttackDamage(unit);
                        var attackHitTime = (creepStatus.LastAttackAnimationTime - creepStatus.TimeBetweenAttacks) + creepStatus.GetAutoAttackArrivalTime(unit);

                        while (attackHitTime <= untilTime)
                        {
                            if (attackHitTime > now)
                            {
                                health -= damage;
                            }

                            attackHitTime += creepStatus.TimeBetweenAttacks;
                        }
                    }
                }
            }

            return health;
        }

        public bool ShouldWait(float t = 2f)
        {
            return EntityManager<Creep>.Entities.Any(
                unit => unit.Team != this.Owner.Team &&
                        this.Owner.IsValidOrbwalkingTarget(unit) &&
                        this.GetPrediction(unit, t / this.Owner.AttacksPerSecond) < this.Owner.GetAttackDamage(unit, true));
        }

        protected override void OnActivate()
        {
            UpdateManager.Subscribe(this.OnUpdate, 1000);
            ObjectManager.OnAddTrackingProjectile += this.OnTrackingProjectile;
            Entity.OnAnimationChanged += this.OnAnimationChanged;
            Entity.OnHandlePropertyChange += this.OnHandleChanged;
        }

        protected override void OnDeactivate()
        {
            UpdateManager.Unsubscribe(this.OnUpdate);
            ObjectManager.OnAddTrackingProjectile -= this.OnTrackingProjectile;
            Entity.OnAnimationChanged -= this.OnAnimationChanged;
            Entity.OnHandlePropertyChange -= this.OnHandleChanged;
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                UpdateManager.Unsubscribe(this.OnUpdate);
                ObjectManager.OnAddTrackingProjectile -= this.OnTrackingProjectile;
                Entity.OnAnimationChanged -= this.OnAnimationChanged;
                Entity.OnHandlePropertyChange -= this.OnHandleChanged;
            }

            this.disposed = true;
        }

        private CreepStatus GetCreepStatusEntry(Unit source)
        {
            var handle = source.Handle;

            if (!this.CreepStatuses.ContainsKey(handle))
            {
                this.CreepStatuses.Add(handle, new CreepStatus(source));
            }

            return this.CreepStatuses[handle];
        }

        private void OnAnimationChanged(Entity sender, EventArgs args)
        {
            var creep = sender as Creep;

            if (creep == null)
            {
                return;
            }

            if (this.Owner.Distance2D(creep) > 3000)
            {
                return;
            }

            if (creep.IsNeutral)
            {
                return;
            }

            if (!creep.Animation.Name.ToLowerInvariant().Contains("attack"))
            {
                return;
            }

            var creepStatus = this.GetCreepStatusEntry(creep);
            creepStatus.LastAttackAnimationTime = Game.RawGameTime - (Game.Ping / 2000f);
        }

        private void OnHandleChanged(Entity sender, HandlePropertyChangeEventArgs args)
        {
            var tower = sender as Tower;

            if (tower == null)
            {
                return;
            }

            if (!args.PropertyName.Equals("m_htowerattacktarget", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            if (this.Owner.Distance2D(tower) > 3000)
            {
                return;
            }

            var creepStatus = this.GetCreepStatusEntry(tower);
            creepStatus.LastAttackAnimationTime = Game.RawGameTime - (Game.Ping / 2000f);
        }

        private void OnTrackingProjectile(TrackingProjectileEventArgs args)
        {
            var sourceCreep = args.Projectile.Source as Creep;
            var sourceTower = args.Projectile.Source as Tower;

            if (sourceCreep != null)
            {
                if (this.Owner.Distance2D(sourceCreep) > 3000)
                {
                    return;
                }

                if (sourceCreep.IsNeutral)
                {
                    return;
                }

                var creepStatus = this.GetCreepStatusEntry(sourceCreep);
                creepStatus.Target = args.Projectile.Target as Creep;
            }
            else if (sourceTower != null)
            {
                if (this.Owner.Distance2D(sourceTower) > 3000)
                {
                    return;
                }

                var creepStatus = this.GetCreepStatusEntry(sourceTower);
                creepStatus.LastAttackAnimationTime = Game.RawGameTime - creepStatus.AttackPoint - (Game.Ping / 2000f);
            }
        }

        private void OnUpdate()
        {
            var toRemove = this.CreepStatuses.Where(pair => !pair.Value.IsValid || this.Owner.Distance2D(pair.Value.Source) > 4000).ToList();

            foreach (var remove in toRemove)
            {
                this.CreepStatuses.Remove(remove.Key);
            }
        }
    }
}