// <copyright file="CreepInfo.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Service;

    using PlaySharp.Toolkit.Helper.Annotations;

    using SharpDX;

    // WIP
    public class CreepWave
    {
        private const float BoostSpeed = 422.5f;

        private const float DireBoostDuration = 8.0f; // Top

        private const float DireSlowDuration = 22.0f; // Bottom

        // 7 minutes is the last boosted wave
        private const float LastSpeedChangeTime = 7.0f * 60.0f;

        private const float NormalSpeed = 325f;

        private const float RadiantBoostDuration = 16.0f; // Bottom

        private const float RadiantSlowDuration = 8.0f; // Top

        private const float SlowSpeed = 211.25f;

        private readonly float modifiedSpeed;

        private readonly float modifiedSpeedDuration;

        private readonly bool moveSpeedBoostActive;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private readonly List<Vector3> route;

        private bool isSpawned;

        public CreepWave(Creep creep, MapArea lane, List<Vector3> route)
        {
            this.route = route;
            this.Team = creep.Team;

            var time = (int)Game.GameTime + 30;
            this.SpawnTime = time - (time % 30);

            this.Lane = lane;

            if (this.Lane == MapArea.Middle)
            {
                this.moveSpeedBoostActive = false;
            }
            else
            {
                this.moveSpeedBoostActive = this.SpawnTime <= LastSpeedChangeTime;
            }

            if (this.Team == Team.Radiant)
            {
                if (this.Lane == MapArea.Top)
                {
                    this.modifiedSpeedDuration = RadiantSlowDuration;
                    this.modifiedSpeed = SlowSpeed;
                }
                else if (this.Lane == MapArea.Bottom)
                {
                    this.modifiedSpeedDuration = RadiantBoostDuration;
                    this.modifiedSpeed = BoostSpeed;
                }
            }
            else if (this.Team == Team.Dire)
            {
                if (this.Lane == MapArea.Top)
                {
                    this.modifiedSpeedDuration = DireBoostDuration;
                    this.modifiedSpeed = BoostSpeed;
                }
                else if (this.Lane == MapArea.Bottom)
                {
                    this.modifiedSpeedDuration = DireSlowDuration;
                    this.modifiedSpeed = SlowSpeed;
                }
            }
            else
            {
                throw new Exception();
            }

            this.AddCreep(creep);
        }

        [NotNull]
        public IReadOnlyCollection<Creep> Creeps
        {
            get
            {
                return this.CreepList.AsReadOnly();
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this creep wave has spawned.
        /// </summary>
        public bool IsSpawned
        {
            get
            {
                if (!this.isSpawned)
                {
                    this.isSpawned = Game.GameTime >= this.SpawnTime || this.Creeps.Any(x => x.IsValid && x.IsSpawned);
                }

                return this.isSpawned;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether at least one unit of this creep wave is visible.
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return this.CreepList.Any(x => x.IsValid && x.IsVisible);
            }
        }

        public MapArea Lane { get; }

        public Vector3 LastVisiblePosition { get; private set; } = Vector3.Zero;

        public float LastVisibleTime { get; private set; } = -1.0f;

        /// <summary>
        ///     Gets the current position of the creep wave.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                if (this.IsVisible)
                {
                    return this.LastVisiblePosition;
                }

                float distance;
                var time = Game.GameTime;
                float modifyDuration;

                if (this.LastVisibleTime < 0)
                {
                    if (!this.moveSpeedBoostActive)
                    {
                        distance = NormalSpeed * (time - this.LastVisibleTime);
                    }
                    else
                    {
                        //if(this.LastVisibleTime >)
                        // changed speed
                        distance = this.modifiedSpeed * this.modifiedSpeedDuration;

                        // rest of duration is normal speed
                        distance += NormalSpeed * (time - this.SpawnTime - this.modifiedSpeedDuration);
                    }

                    // get closest pos on route
                    int closestIndex;
                    var closestPoint = Geometry.Geometry.GetClosestPoint(this.route, this.LastVisiblePosition, out closestIndex);

                    // add distance back to route
                    distance += (closestPoint - this.LastVisiblePosition).Length();

                    for (var i = closestIndex; i < (this.route.Count - 1); ++i)
                    {
                        var p1 = this.route[i];
                        var p2 = this.route[i + 1];

                        var dir = p2 - p1;
                        var len = dir.Length();


                        if (len <= distance)
                        {
                            distance -= len;
                        }
                        else
                        {
                            return p1 + (dir.Normalized() * distance);
                        }
                    }
                }
                else
                {
                    // it's after 7:00 so speed change isn't active anymore
                    if (!this.moveSpeedBoostActive)
                    {
                        distance = NormalSpeed * (time - this.SpawnTime);
                    }
                    else
                    {
                        var ms = this.GetMoveSpeed(this.SpawnTime, out modifyDuration);
                        if (this.IsMoveSpeedChangeActive)
                        {
                            distance = this.modifiedSpeed * (time - this.SpawnTime);
                        }
                        else
                        {
                            // changed speed
                            distance = this.modifiedSpeed * this.modifiedSpeedDuration;

                            // rest of duration is normal speed
                            distance += NormalSpeed * (time - this.SpawnTime - this.modifiedSpeedDuration);
                        }
                    }

                    for (var i = 0; i < (this.route.Count - 1); ++i)
                    {
                        var p1 = this.route[i];
                        var p2 = this.route[i + 1];

                        var dir = p2 - p1;
                        var len = dir.Length();

                        if (len <= distance)
                        {
                            distance -= len;
                        }
                        else
                        {
                            return p1 + (dir.Normalized() * distance);
                        }
                    }
                }

                return this.route.Last();
            }
        }

        /// <summary>
        ///     Gets the time in seconds when this creep wave will spawn or has spawned.
        /// </summary>
        public float SpawnTime { get; }

        /// <summary>
        ///     Gets the team of this creep wave
        /// </summary>
        public Team Team { get; }

        private List<Creep> CreepList { get; } = new List<Creep>();

        private bool IsMoveSpeedChangeActive
        {
            get
            {
                return this.moveSpeedBoostActive && (this.modifiedSpeedDuration - (Game.GameTime - this.SpawnTime)) > 0;
            }
        }

        public void AddCreep([NotNull] Creep creep)
        {
            this.CreepList.Add(creep);
        }

        public void OnUpdate()
        {
            // Update last visible position
            if (this.IsVisible)
            {
                this.LastVisibleTime = Game.GameTime;
                this.LastVisiblePosition = this.GetMiddlePosition();
            }
        }

        public Vector3 PredictPosition(int ms)
        {
            if (ms <= 0)
            {
                return this.Position;
            }

            return Vector3.Down;
        }

        public void RemoveCreep([NotNull] Creep creep)
        {
            this.CreepList.Remove(creep);
        }

        private Vector3 GetMiddlePosition()
        {
            var creeps = this.CreepList.Where(x => x.IsValid && x.IsVisible && x.IsAlive).ToList();
            var count = creeps.Count;
            if (count == 0)
            {
                return Vector3.Zero;
            }

            var pos = creeps.Aggregate(Vector3.Zero, (current, creep) => current + creep.NetworkPosition);
            return pos / creeps.Count;
        }

        private float GetMoveSpeed(out float boostDuration)
        {
            return this.GetMoveSpeed(Game.GameTime, out boostDuration);
        }

        private float GetMoveSpeed(float time, out float modifyDuration)
        {
            if (!this.moveSpeedBoostActive)
            {
                modifyDuration = 0.0f;
                return NormalSpeed;
            }

            var timeDiff = time - this.SpawnTime;
            if (timeDiff >= this.modifiedSpeedDuration)
            {
                modifyDuration = 0.0f;
                return NormalSpeed;
            }

            modifyDuration = this.modifiedSpeedDuration - timeDiff;
            return this.modifiedSpeed;
        }
    }

    [Export(typeof(CreepInfo))]
    public sealed class CreepInfo : ControllableService
    {
        private Map map;

        [ImportingConstructor]
        public CreepInfo([Import] Map map)
        {
            this.map = map;
        }

        public List<CreepWave> CreepWaves { get; } = new List<CreepWave>();

        protected override void OnActivate()
        {
            this.CreepWaves.Clear();

            UpdateManager.Subscribe(this.OnUpdate, 250);
            EntityManager<Creep>.EntityAdded += this.OnEntityAdded;
            EntityManager<Creep>.EntityRemoved += this.OnEntityRemoved;
        }

        protected override void OnDeactivate()
        {
            UpdateManager.Unsubscribe(this.OnUpdate);
            EntityManager<Creep>.EntityAdded -= this.OnEntityAdded;
            EntityManager<Creep>.EntityRemoved -= this.OnEntityRemoved;

            this.CreepWaves.Clear();
        }

        private void OnEntityAdded(object sender, Creep creep)
        {
            if (!creep.IsValid)
            {
                return;
            }

            var creepTeam = creep.Team;
            if (creepTeam != Team.Dire && creepTeam != Team.Radiant)
            {
                return;
            }

            var wave = this.CreepWaves.FirstOrDefault(x => !x.IsSpawned && x.Creeps.Any(y => y.Distance2D(creep) < 300));
            if (wave != null)
            {
                wave.AddCreep(creep);
            }
            else
            {
                var lane = this.map.GetLane(creep);
                var route = this.map.GetCreepRoute(creep, lane);
                if (route.Count > 0)
                {
                    wave = new CreepWave(creep, lane, route);
                    this.CreepWaves.Add(wave);
                }
            }
        }

        private void OnEntityRemoved(object sender, Creep creep)
        {
            if (!creep.IsValid)
            {
                return;
            }

            var creepTeam = creep.Team;
            if (creepTeam != Team.Dire && creepTeam != Team.Radiant)
            {
                return;
            }

            var wave = this.CreepWaves.FirstOrDefault(x => x.Creeps.Contains(creep));
            wave?.RemoveCreep(creep);

            // remove empty waves
            this.CreepWaves.RemoveAll(x => x.Creeps.Count == 0);
        }

        private void OnUpdate()
        {
            // Merge existing waves
            var groups = this.CreepWaves.Where(x => x.IsSpawned).GroupBy(x => x.Team).ToList();
            foreach (var group in groups)
            {
                foreach (var creepWave in group)
                {
                    var merge = group.FirstOrDefault(x => !x.Equals(creepWave) && x.Position.Distance(creepWave.Position) < 500);

                    if (merge != null)
                    {
                        merge.Creeps.ForEach(x => creepWave.AddCreep(x));
                        this.CreepWaves.Remove(merge);
                        break;
                    }
                }
            }

            // Update last visible position
            foreach (var creepWave in this.CreepWaves)
            {
                creepWave.OnUpdate();
            }
        }
    }
}