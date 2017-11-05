// <copyright file="ActiveAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using log4net;

    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    public abstract class ActiveAbility : BaseAbility
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected ActiveAbility(Ability ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                if (!this.IsReady)
                {
                    return false;
                }

                var owner = this.Owner;
                var isItem = this.Ability is Item;
                if (owner.IsStunned() || isItem && owner.IsMuted() || !isItem && owner.IsSilenced())
                {
                    return false;
                }

                if ((Game.RawGameTime - this.LastCastAttempt) < 0.1f)
                {
                    //Log.Debug($"blocked {this}");
                    return false;
                }

                return true;
            }
        }

        public virtual float CastPoint
        {
            get
            {
                const float MinimumCastPoint = 0.05f;
                if (this.Ability is Item)
                {
                    // 50 ms
                    return MinimumCastPoint;
                }

                var level = this.Ability.Level;
                if (level == 0)
                {
                    return 0.0f;
                }

                var castpoint = this.Ability.GetCastPoint(level - 1);
                return castpoint == 0.0f ? MinimumCastPoint : castpoint;
            }
        }

        public virtual bool IsActivated
        {
            get
            {
                return this.Ability.IsActivated;
            }
        }

        public virtual float Speed
        {
            get
            {
                return float.MaxValue;
            }
        }

        protected float LastCastAttempt { get; set; }

        protected override float RawDamage
        {
            get
            {
                var level = this.Ability.Level;
                if (level == 0)
                {
                    return 0;
                }

                var damage = (float)this.Ability.GetDamage(level - 1);
                return damage;
            }
        }

        public static implicit operator bool(ActiveAbility ability)
        {
            return ability.CanBeCasted;
        }

        public virtual bool CanHit(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return true;
            }

            if (this.Owner.Distance2D(targets.First()) < this.CastRange)
            {
                return true;
            }

            // moar checks
            return false;
        }

        /// <summary>
        ///     Gets the time needed to execute an ability. This assumes that you are already in range and includes turnrate,
        ///     castpoint and ping.
        /// </summary>
        /// <param name="target">The target of the ability.</param>
        /// <returns>Time in ms until the cast.</returns>
        public virtual int GetCastDelay(Unit target)
        {
            return (int)(((this.CastPoint + this.Owner.TurnTime(target.NetworkPosition)) * 1000.0f) + Game.Ping);
        }

        /// <summary>
        ///     Gets the time needed to execute an ability. This assumes that you are already in range and includes turnrate,
        ///     castpoint and ping.
        /// </summary>
        /// <param name="position">The target position of the ability.</param>
        /// <returns>Time in ms until the cast.</returns>
        public virtual int GetCastDelay(Vector3 position)
        {
            return (int)(((this.CastPoint + this.Owner.TurnTime(position)) * 1000.0f) + Game.Ping);
        }

        /// <summary>
        ///     Gets the time needed to execute an ability. This assumes that you are already in range and includes castpoint and
        ///     ping.
        /// </summary>
        /// <returns>Time in ms until the cast.</returns>
        public virtual int GetCastDelay()
        {
            return (int)((this.CastPoint * 1000.0f) + Game.Ping);
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = this.RawDamage;
            if (damage == 0)
            {
                return 0;
            }

            var amplify = this.Owner.GetSpellAmplification();
            var reduction = 0.0f;
            if (targets.Any())
            {
                reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public override float GetDamage([NotNull] Unit target, float damageModifier, float targetHealth = float.MinValue)
        {
            var damage = this.RawDamage;
            if (damage == 0)
            {
                return 0;
            }

            var amplify = this.Owner.GetSpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target, this.DamageType);

            return DamageHelpers.GetSpellDamage(damage, amplify, -reduction, damageModifier);
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
                return this.GetCastDelay(position) + (int)this.ActivationDelay;
            }

            var time = this.Owner.Distance2D(position) / this.Speed;
            return this.GetCastDelay(position) + (int)(time * 1000.0f) + (int)this.ActivationDelay;
        }

        public virtual bool UseAbility()
        {
            if (!this.CanBeCasted)
            {
                Log.Debug($"blocked {this}");
                return false;
            }

            var result = this.Ability.UseAbility();
            if (result)
            {
                this.LastCastAttempt = Game.RawGameTime;
            }

            return result;
        }

        public virtual bool UseAbility(Unit target)
        {
            if (!this.CanBeCasted)
            {
                Log.Debug($"blocked {this}");
                return false;
            }

            bool result;
            if ((this.Ability.AbilityBehavior & AbilityBehavior.UnitTarget) == AbilityBehavior.UnitTarget)
            {
                result = this.Ability.UseAbility(target);
            }
            else if ((this.Ability.AbilityBehavior & AbilityBehavior.Point) == AbilityBehavior.Point)
            {
                result = this.Ability.UseAbility(target.NetworkPosition);
            }
            else
            {
                result = this.Ability.UseAbility();
            }

            if (result)
            {
                this.LastCastAttempt = Game.RawGameTime;
            }

            return result;
        }

        public virtual bool UseAbility(Tree target)
        {
            if (!this.CanBeCasted)
            {
                Log.Debug($"blocked {this}");
                return false;
            }

            var result = this.Ability.UseAbility(target);
            if (result)
            {
                this.LastCastAttempt = Game.RawGameTime;
            }

            return result;
        }

        public virtual bool UseAbility(Vector3 position)
        {
            if (!this.CanBeCasted)
            {
                Log.Debug($"blocked {this}");
                return false;
            }

            bool result;
            if ((this.Ability.AbilityBehavior & AbilityBehavior.Point) == AbilityBehavior.Point)
            {
                result = this.Ability.UseAbility(position);
            }
            else
            {
                result = this.Ability.UseAbility();
            }

            if (result)
            {
                this.LastCastAttempt = Game.RawGameTime;
            }

            return result;
        }

        public virtual bool UseAbility(Rune target)
        {
            if (!this.CanBeCasted)
            {
                Log.Debug($"blocked {this}");
                return false;
            }

            var result = this.Ability.UseAbility(target);
            if (result)
            {
                this.LastCastAttempt = Game.RawGameTime;
            }

            return result;
        }
    }
}