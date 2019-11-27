// <copyright file="void_spirit_aether_remnant.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_void_spirit
{
    using System;

    using Ensage.SDK.Extensions;

    using SharpDX;

    public class void_spirit_aether_remnant : LineAbility
    {
        public void_spirit_aether_remnant(Ability ability)
            : base(ability)
        {
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
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("projectile_speed");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("impact_damage");
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
            var distance = Math.Max(this.Owner.AttackRange(), this.Owner.Distance2D(position) - this.CastRange);
            var startPosition = position.Extend(this.Owner.Position, distance);

            return this.UseAbility(startPosition, position);
        }
    }
}