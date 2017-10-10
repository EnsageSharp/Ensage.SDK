// <copyright file="OrbAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using System;
    using System.Linq;

    using Ensage.SDK.Extensions;

    public abstract class OrbAbility : AutocastAbility
    {
        protected OrbAbility(Ability ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return this.Owner.CanAttack() && base.CanBeCasted;
            }
        }

        public override float CastPoint
        {
            get
            {
                return this.Owner.AttackPoint();
            }
        }

        public override float CastRange
        {
            get
            {
                return this.Owner.AttackRange();
            }
        }

        public override float Speed
        {
            get
            {
                return this.Owner.ProjectileSpeed();
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var owner = this.Owner;
            if (!targets.Any())
            {
                return owner.MinimumDamage + owner.BonusDamage;
            }

            return owner.GetAttackDamage(targets.First());
        }

        public override float GetDamage(Unit target, float physicalDamageModifier, float targetHealth = float.MinValue)
        {
            return this.Owner.GetAttackDamage(target, false, physicalDamageModifier);
        }
    }
}