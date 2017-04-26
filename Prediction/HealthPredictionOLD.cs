// <copyright file="HealthPredictionOLD.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Prediction
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using SharpDX;

    public class HealthPredictionOLD
    {
        private static HealthPredictionOLD _Instance;

        private float _LastUpdateTime;

        private Dictionary<uint, CreepStatus> CreepStatuses = new Dictionary<uint, CreepStatus>();

        public Hero Hero
        {
            get
            {
                return ObjectManager.LocalHero;
            }
        }

        private bool _Initialized { get; set; }

        public static HealthPredictionOLD Instance()
        {
            if (_Instance == null)
            {
                _Instance = new HealthPredictionOLD();
            }

            return _Instance;
        }

        public float GetPredictedHealth(Unit unit, float untilTime)
        {
            var now = Game.RawGameTime;
            var health = (float)unit.Health;
            untilTime = Math.Max(0f, untilTime);
            untilTime = now + untilTime;

            var team = unit.Team;
            foreach (var creepStatusValuePair in this.CreepStatuses)
            {
                var creepStatus = creepStatusValuePair.Value;

                if (!creepStatus.IsValid)
                {
                    continue;
                }

                if (creepStatus.Team == team)
                {
                    continue;
                }

                var targetCreep = creepStatus.Target;
                if (targetCreep == null || targetCreep.Handle != unit.Handle)
                {
                    continue;
                }

                var damage = creepStatus.Source.GetAttackDamage(unit);

                float attackHitTime;

                if (creepStatus.LastAttackAnimationTime == 0f
                    || (now - creepStatus.LastAttackAnimationTime) > (creepStatus.TimeBetweenAttacks + 0.2))
                {
                    continue;
                }

                // handle melee creeps
                if (creepStatus.IsMelee)
                {
                    attackHitTime = creepStatus.LastAttackAnimationTime + creepStatus.AttackPoint;
                }

                // ranged creeps
                else
                {
                    attackHitTime = (creepStatus.LastAttackAnimationTime - creepStatus.TimeBetweenAttacks)
                                    + creepStatus.GetAutoAttackArrivalTime(unit);
                }

                var i = 0;
                while (attackHitTime <= untilTime)
                {
                    if (attackHitTime > now)
                    {
                        health -= damage;
                    }

                    attackHitTime += creepStatus.TimeBetweenAttacks;
                    i++;
                }
            }

            if (health > 0f)
            {
                // towers
                var closestTower =
                    EntityManager<Tower>.Entities
                                        .OrderBy(tower => tower.IsValid ? tower.Distance2D(this.Hero) : float.MaxValue)
                                        .FirstOrDefault(t => t.IsValid);
                if (closestTower != null)
                {
                    var towerTarget = closestTower.AttackTarget;
                    if (towerTarget != null && towerTarget == unit)
                    {
                        var creepStatus = this.GetCreepStatusEntry(closestTower);
                        var damage = closestTower.GetAttackDamage(unit);
                        var attackHitTime = (creepStatus.LastAttackAnimationTime - creepStatus.TimeBetweenAttacks)
                                            + creepStatus.GetAutoAttackArrivalTime(unit);

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

        public bool Load()
        {
            if (this._Initialized)
            {
                return false;
            }

            this._Initialized = true;

            ObjectManager.OnAddTrackingProjectile += this.ObjectManagerOnOnAddTrackingProjectile;
            Game.OnIngameUpdate += this.Game_OnIngameUpdate;
            Drawing.OnDraw += this.DrawingOnOnDraw;
            Entity.OnAnimationChanged += this.Unit_OnAnimationChanged;
            Entity.OnHandlePropertyChange += this.Tower_OnHandlePropertyChange;
            return true;
        }

        public bool Unload()
        {
            if (!this._Initialized)
            {
                return false;
            }

            this._Initialized = false;
            ObjectManager.OnAddTrackingProjectile -= this.ObjectManagerOnOnAddTrackingProjectile;
            Game.OnIngameUpdate -= this.Game_OnIngameUpdate;
            Entity.OnAnimationChanged -= this.Unit_OnAnimationChanged;
            return true;
        }

        void DrawingOnOnDraw(EventArgs args)
        {
            return;
            var pTeam = this.Hero.Team;
            foreach (var senderCreep in EntityManager<Creep>.Entities
                                                            .Where(
                                                                unit =>
                                                                    unit.Team == pTeam && unit.Distance2D(this.Hero) < 3000f))
            {
                var creepStatus = this.GetCreepStatusEntry(senderCreep);
                var targetCreep = creepStatus.Target;

                if (!creepStatus.IsValid)
                {
                    // continue;
                }

                if (creepStatus.LastAttackAnimationTime == 0f)
                {
                    // continue;
                }

                var position = Drawing.WorldToScreen(senderCreep.Position);
                var text = "Status: " + senderCreep.Handle + " HasTarget: " + (targetCreep != null)
                           + " TimeBetweenAttacks: " + creepStatus.TimeBetweenAttacks + " Animation: "
                           + (Game.RawGameTime - creepStatus.LastAttackAnimationTime) + " MissileSpeed: "
                           + creepStatus.MissileSpeed;

                Drawing.DrawText(text, position, Color.White, FontFlags.AntiAlias);

                if (targetCreep != null)
                {
                    var position2 = Drawing.WorldToScreen(targetCreep.Position)
                                    + new Vector2(0, 10 * (senderCreep.Handle % 3));
                    var time = this.Hero.GetAutoAttackArrivalTime(targetCreep);
                    Drawing.DrawText(
                        "Target of " + senderCreep.Handle + " predicted health: "
                        + this.GetPredictedHealth(targetCreep, time) + " vs " + targetCreep.Health + "(" + time + ") "
                        + "inrange: " + this.Hero.IsInAttackRange(targetCreep) + " dis: "
                        + (this.Hero.HullRadius + targetCreep.HullRadius + this.Hero.Distance2D(targetCreep))
                        + " range: " + this.Hero.AttackRange(targetCreep),
                        position2,
                        Color.White,
                        FontFlags.AntiAlias);

                    Drawing.DrawLine(position, position2, Color.White);
                }
            }

            var closestTower =
                EntityManager<Tower>.Entities
                                    .OrderBy(tower => tower.IsValid ? tower.Distance2D(this.Hero) : float.MaxValue)
                                    .FirstOrDefault(t => t.IsValid);
            if (closestTower != null)
            {
                var towerTarget = closestTower.AttackTarget;
                if (towerTarget != null)
                {
                    var creepStatus = this.GetCreepStatusEntry(closestTower);
                    var position = Drawing.WorldToScreen(closestTower.Position);
                    var text = "Status: " + closestTower.Handle + " HasTarget: " + (towerTarget != null)
                               + " TimeBetweenAttacks: " + creepStatus.TimeBetweenAttacks + " Animation: "
                               + (Game.RawGameTime - creepStatus.LastAttackAnimationTime) + " Attackpoint: "
                               + creepStatus.AttackPoint;

                    Drawing.DrawText(text, position, Color.White, FontFlags.AntiAlias);

                    var position2 = Drawing.WorldToScreen(towerTarget.Position)
                                    + new Vector2(0, 10 * (towerTarget.Handle % 3));
                    var time = this.Hero.GetAutoAttackArrivalTime(towerTarget);
                    Drawing.DrawText(
                        "Target of " + towerTarget.Handle + " predicted health: "
                        + this.GetPredictedHealth(towerTarget, time) + " vs " + towerTarget.Health + "(" + time + ")",
                        position2,
                        Color.White,
                        FontFlags.AntiAlias);

                    Drawing.DrawLine(position, position2, Color.White);
                }
            }
        }

        void Game_OnIngameUpdate(EventArgs args)
        {
            var now = Game.RawGameTime;
            if ((now - this._LastUpdateTime) < 1)
            {
                return;
            }

            this._LastUpdateTime = now;
            var toRemove =
                this.CreepStatuses.Where(pair => !pair.Value.IsValid || this.Hero.Distance2D(pair.Value.Source) > 4000)
                    .ToList();
            foreach (var remove in toRemove)
            {
                this.CreepStatuses.Remove(remove.Key);
            }
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

        private void ObjectManagerOnOnAddTrackingProjectile(TrackingProjectileEventArgs args)
        {
            var sourceCreep = args.Projectile.Source as Creep;
            var sourceTower = args.Projectile.Source as Tower;

            if (sourceCreep != null)
            {
                if (this.Hero.Distance2D(sourceCreep) > 3000)
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
                if (this.Hero.Distance2D(sourceTower) > 3000)
                {
                    return;
                }

                var creepStatus = this.GetCreepStatusEntry(sourceTower);
                creepStatus.LastAttackAnimationTime = Game.RawGameTime - creepStatus.AttackPoint - (Game.Ping / 2000f);
            }
        }

        private void Tower_OnHandlePropertyChange(Entity sender, HandlePropertyChangeEventArgs args)
        {
            if (!(sender is Tower))
            {
                return;
            }

            if (!args.PropertyName.Equals("m_htowerattacktarget", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            if (this.Hero.Distance2D(sender) > 3000)
            {
                return;
            }

            var tower = sender as Tower;
            var creepStatus = this.GetCreepStatusEntry(tower);
            creepStatus.LastAttackAnimationTime = Game.RawGameTime - (Game.Ping / 2000f);
        }

        private void Unit_OnAnimationChanged(Entity sender, EventArgs args)
        {
            if (!(sender is Creep))
            {
                return;
            }

            if (this.Hero.Distance2D(sender) > 3000)
            {
                return;
            }

            var senderCreep = sender as Creep;

            if (senderCreep.IsNeutral)
            {
                return;
            }

            if (!senderCreep.Animation.Name.ToLowerInvariant().Contains("attack"))
            {
                return;
            }

            var creepStatus = this.GetCreepStatusEntry(senderCreep);
            creepStatus.LastAttackAnimationTime = Game.RawGameTime - (Game.Ping / 2000f);
        }
    }
}