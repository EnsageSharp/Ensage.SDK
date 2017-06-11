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

    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    public abstract class ActiveAbility : BaseAbility
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected ActiveAbility(Ability ability)
            : base(ability)
        {
        }

        public bool CanBeCasted
        {
            get
            {
                if (this.Ability.Level == 0 || this.Ability.IsHidden || this.Ability.Cooldown > 0)
                {
                    return false;
                }

                var owner = this.Owner;
                if (owner.Mana < this.Ability.ManaCost || owner.IsSilenced())
                {
                    // TODO: check for item
                    return false;
                }

                if ((Game.RawGameTime - this.LastCastAttempt) < 0.1f)
                {
                    Log.Debug($"blocked {this}");
                    return false;
                }

                return true;
            }
        }

        public virtual float CastPoint
        {
            get
            {
                var level = this.Ability.Level;
                if (level == 0)
                {
                    return 0.0f;
                }

                return this.Ability.GetCastPoint(level - 1);
            }
        }

        protected float LastCastAttempt { get; set; }

        public static implicit operator bool(ActiveAbility ability)
        {
            return ability.CanBeCasted;
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
            var damage = this.GetRawDamage();
            if (damage == 0)
            {
                return 0;
            }

            var amplify = this.Owner.GetSpellAmplification();
            var reduction = 0.0f;

            if (targets.Any())
            {
                reduction = this.Ability.GetDamageReduction(targets.First());
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
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

            var result = this.Ability.UseAbility(target);
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

            var result = this.Ability.UseAbility(position);
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

        protected override float GetRawDamage()
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
}