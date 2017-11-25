// <copyright file="pangolier_swashbuckle.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_pangolier
{
    using System;

    using Ensage.SDK.Extensions;

    using SharpDX;

    public class pangolier_swashbuckle : LineAbility
    {
        public pangolier_swashbuckle(Ability ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return base.CanBeCasted && !this.Owner.IsRooted();
            }
        }

        public override float EndRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("end_radius");
            }
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("start_radius");
            }
        }

        public override float Range
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("range");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("dash_speed");
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("dash_range");
            }
        }

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "damage");
                var strikes = this.Ability.GetAbilitySpecialData("strikes");

                return damage * strikes;
            }
        }

        public bool UseAbility(Vector3 startPosition, Vector3 direction)
        {
            if (!this.CanBeCasted)
            {
                return false;
            }

            if (!this.Ability.TargetPosition(startPosition) || !this.Ability.TargetPosition(direction))
            {
                return false;
            }

            var result = this.Ability.UseAbility(startPosition);

            if (result)
            {
                this.LastCastAttempt = Game.RawGameTime;
            }

            return result;
        }

        public override bool UseAbility(Vector3 position)
        {
            // simple position to get as close as possible to target
            var distance = Math.Max(this.Owner.AttackRange(), this.Owner.Distance2D(position) - this.CastRange);
            var startPosition = position.Extend(this.Owner.Position, distance);

            return this.UseAbility(startPosition, position);
        }
    }
}