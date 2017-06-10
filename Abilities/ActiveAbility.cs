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

        protected float LastCastAttempt;

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

        public override float GetDamage(params Unit[] targets)
        {
            var level = this.Ability.Level;
            if (level == 0)
            {
                return 0;
            }

            var damage = (float)this.Ability.GetDamage(level - 1);
            damage *= 1.0f + this.Owner.GetSpellAmplification();

            if (targets.Any())
            {
                damage *= 1.0f - this.Ability.GetDamageReduction(targets.First());
            }

            return damage;
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
    }
}