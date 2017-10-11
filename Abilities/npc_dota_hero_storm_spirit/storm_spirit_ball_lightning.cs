// <copyright file="storm_spirit_ball_lightning.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_storm_spirit
{
    using System;
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using SharpDX;

    public class storm_spirit_ball_lightning : RangedAbility, IHasModifier, IAreaOfEffectAbility
    {
        public storm_spirit_ball_lightning(Ability ability)
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

        public override float CastRange
        {
            get
            {
                var mana = this.Owner.Mana - this.Ability.ManaCost;
                if (mana < 0)
                {
                    return 0;
                }

                var travelCost = this.Ability.GetAbilitySpecialData("ball_lightning_travel_cost_base")
                                 + (this.Owner.MaximumMana * (this.Ability.GetAbilitySpecialData("ball_lightning_travel_cost_percent") / 100));

                return (float)Math.Ceiling(mana / travelCost) * 100;
            }
        }

        public string ModifierName { get; } = "storm_spirit_ball_lightning";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("ball_lightning_aoe");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("ball_lightning_move_speed");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var target = targets.FirstOrDefault();
            if (target == null)
            {
                return this.RawDamage;
            }

            var distance = Math.Max(this.Owner.Distance2D(target) - this.Radius, 0) / 100;
            var amplify = this.Owner.GetSpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
            var damage = DamageHelpers.GetSpellDamage(this.RawDamage * distance, amplify, reduction);

            return damage;
        }

        public float ManaCost(Vector3 position)
        {
            var travelCost = this.Ability.GetAbilitySpecialData("ball_lightning_travel_cost_base")
                             + (this.Owner.MaximumMana * (this.Ability.GetAbilitySpecialData("ball_lightning_travel_cost_percent") / 100));
            var distance = (int)Math.Floor(this.Owner.Distance2D(position) / 100);

            return this.Ability.ManaCost + (distance * travelCost);
        }
    }
}