// <copyright file="ActiveAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using SharpDX;

    public abstract class ActiveAbility : BaseAbility, IActiveAbility
    {
        protected ActiveAbility(Ability ability)
            : base(ability)
        {
        }

        public virtual bool CanBeCasted
        {
            get
            {
                if (this.Ability.IsHidden)
                {
                    return false;
                }

                var unit = this.Ability.Owner as Unit;
                if (unit != null)
                {
                    if (this.IsItem)
                    {
                        if (unit.IsMuted())
                        {
                            return false;
                        }
                    }
                    else if (unit.IsSilenced())
                    {
                        return true;
                    }

                    if (unit.Mana < this.Ability.ManaCost)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public virtual float CastPoint
        {
            get
            {
                if (this.Ability is Item)
                {
                    return 0.0f;
                }

                var level = this.Ability.Level;
                if (level == 0)
                {
                    return 0.0f;
                }

                return this.Ability.GetCastPoint(level - 1);
            }
        }

        public virtual bool IsChanneling { get; } = false;

        public override float Range => this.Ability.CastRange;

        public virtual float Speed => float.MaxValue;

        public override bool CanHitTarget(Unit target)
        {
            if (!this.CanAffectTarget(target))
            {
                return false;
            }

            var entity = this.Ability.Owner;
            if (entity != null)
            {
                var range = this.Range;
                if (target.Distance2D(entity) > range)
                {
                    return false;
                }
            }

            return true;
        }

        public override float GetDamage(params Unit[] target)
        {
            var level = this.Ability.Level;
            return level == 0 ? 0.0f : this.Ability.GetDamage(level - 1);
        }

        public virtual float GetTravelTime(EntityOrPosition target)
        {
            var speed = this.Speed;
            if (speed == float.MaxValue)
            {
                return 0.0f;
            }

            return this.Ability.Owner.Distance2D(target) / speed;
        }

        public virtual float GetTravelTime()
        {
            var speed = this.Speed;
            if (speed == float.MaxValue)
            {
                return 0.0f;
            }

            return this.Range / speed;
        }

        public virtual float GetTravelTime(Entity target)
        {
            var speed = this.Speed;
            if (speed == float.MaxValue)
            {
                return 0.0f;
            }

            return this.Ability.Owner.Distance2D(target) / speed;
        }

        public void UseAbility(bool queued = false)
        {
            this.Ability.UseAbility(queued);
        }

        public void UseAbility(Unit target, bool queued = false)
        {
            this.Ability.UseAbility(target, queued);
        }

        public void UseAbility(Vector3 target, bool queued = false)
        {
            this.Ability.UseAbility(target, queued);
        }
    }
}