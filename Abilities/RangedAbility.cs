// <copyright file="RangedAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using Ensage.SDK.Extensions;

    using SharpDX;

    public abstract class RangedAbility : ActiveAbility
    {
        protected RangedAbility(Ability ability)
            : base(ability)
        {
        }

        public override float Range
        {
            get
            {
                return this.Ability.GetCastRange();
            }
        }

        public virtual float Speed
        {
            get
            {
                return float.MaxValue;
            }
        }

        /// <summary>
        ///     Gets the time until the ability lands on the target. This includes the cast time and assumes that you are in range
        ///     to cast.
        /// </summary>
        /// <param name="target">The target of your ability.</param>
        /// <returns>Time until the spell hits in ms.</returns>
        public virtual int GetHitTime(Unit target)
        {
            return this.GetHitTime(target.NetworkPosition);
        }

        /// <summary>
        ///     Gets the time until the ability lands on the target position. This includes the cast time and assumes that you are
        ///     in range to cast.
        /// </summary>
        /// <param name="position">The target position of your ability.</param>
        /// <returns>Time until the spell hits in ms.</returns>
        public virtual int GetHitTime(Vector3 position)
        {
            if (this.Speed == float.MaxValue || this.Speed == 0)
            {
                return this.GetCastDelay(position);
            }

            var time = this.Owner.Distance2D(position) / this.Speed;
            return this.GetCastDelay(position) + (int)(time * 1000.0f);
        }
    }
}