// <copyright file="CreepStatus.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System.Linq;

    using Ensage.SDK.Extensions;

    internal sealed class CreepStatus
    {
        private float _attackPoint;

        private bool _isMelee;

        private bool _isMeleeCached;

        private bool _isSiege;

        private bool _isSiegeCached;

        private bool _isTower;

        private bool _isTowerCached;

        private bool _isValid = true;

        private Unit _lastPossibleTarget;

        private float _missileSpeed;

        private Unit _target;

        private Team _team = Team.Undefined;

        private float _timeBetweenAttacks;

        public CreepStatus(Unit source)
        {
            this.Source = source;
        }

        public float AttackPoint
        {
            get
            {
                if (this._attackPoint == 0f)
                {
                    this._attackPoint = (float)this.Source.AttackPoint();
                }

                return this._attackPoint;
            }
        }

        public bool IsMelee
        {
            get
            {
                if (!this._isMeleeCached)
                {
                    this._isMelee = this.Source.IsMelee;
                    this._isMeleeCached = true;
                }

                return this._isMelee;
            }
        }

        public bool IsSiege
        {
            get
            {
                if (!this._isSiegeCached)
                {
                    this._isSiege = this.Source.Name.Contains("siege");
                    this._isSiegeCached = true;
                }

                return this._isSiege;
            }
        }

        public bool IsTower
        {
            get
            {
                if (!this._isTowerCached)
                {
                    this._isTower = this.Source as Tower != null;
                    this._isTowerCached = true;
                }

                return this._isTower;
            }
        }

        public bool IsValid
        {
            get
            {
                if (!this._isValid)
                {
                    return false;
                }

                if (!this.Source.IsValid || !this.Source.IsAlive)
                {
                    return false;
                }

                return this._isValid;
            }
        }

        public float LastAttackAnimationTime { get; set; }

        public float MissileSpeed
        {
            get
            {
                if (this._missileSpeed == 0f)
                {
                    if (this.IsMelee)
                    {
                        this._missileSpeed = float.MaxValue;
                    }
                    else
                    {
                        this._missileSpeed = (float)this.Source.ProjectileSpeed();
                    }
                }

                return this._missileSpeed;
            }
        }

        public Unit Source { get; set; }

        public Unit Target
        {
            get
            {
                if (this._target != null && this._target.IsValid && this.Source.IsValidOrbwalkingTarget(this._target))
                {
                    return this._target;
                }

                if (this._lastPossibleTarget != null &&
                    this._lastPossibleTarget.IsValid &&
                    this.Source.IsDirectlyFacing(this._lastPossibleTarget) &&
                    this.Source.IsValidOrbwalkingTarget(this._lastPossibleTarget))
                {
                    return this._lastPossibleTarget;
                }

                var possibleTarget = EntityManager<Creep>.Entities.FirstOrDefault(
                    unit => unit.IsValid &&
                            unit.Team != this.Team &&
                            this.Source.IsDirectlyFacing(unit) &&
                            this.Source.IsValidOrbwalkingTarget(unit));

                if (possibleTarget != null)
                {
                    this._lastPossibleTarget = possibleTarget;
                    return possibleTarget;
                }

                return null;
            }

            set
            {
                this._target = value;
            }
        }

        public Team Team
        {
            get
            {
                if (this._team == Team.Undefined)
                {
                    this._team = this.Source.Team;
                }

                return this._team;
            }
        }

        public float TimeBetweenAttacks
        {
            get
            {
                if (this._timeBetweenAttacks == 0f)
                {
                    this._timeBetweenAttacks = 1 / this.Source.AttacksPerSecond;
                }

                return this._timeBetweenAttacks;
            }
        }

        public float GetAutoAttackArrivalTime(Unit target)
        {
            var result = this.Source.GetProjectileArrivalTime(target, this.AttackPoint, this.MissileSpeed, !this.IsTower);

            if (this.IsTower)
            {
                result += 0.15f;
            }
            else
            {
                result -= 0.10f;
            }

            return result;
        }
    }
}