// <copyright file="crystal_maiden_freezing_field.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_crystal_maiden
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class crystal_maiden_freezing_field : ActiveAbility, IAreaOfEffectAbility, IHasModifier, IChannable
    {
        public crystal_maiden_freezing_field(Ability ability)
            : base(ability)
        {
        }

        public float Duration
        {
            get
            {
                var level = this.Ability.Level;
                if (level == 0)
                {
                    return 0;
                }

                return this.Ability.GetChannelTime(level - 1);
            }
        }

        public float ExplosionRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("explosion_radius");
            }
        }

        public bool IsChanneling
        {
            get
            {
                return this.Ability.IsChanneling;
            }
        }

        public float MaximumExplosionDistance
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("explosion_max_dist");
            }
        }

        public float MinimumExplosionDistance
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("explosion_min_dist");
            }
        }

        public string ModifierName { get; } = "crystal_maiden_freezing_field";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public float RemainingDuration
        {
            get
            {
                if (!this.IsChanneling)
                {
                    return 0;
                }

                return this.Duration - this.Ability.ChannelTime;
            }
        }

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("damage");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_crystal_maiden_3);
                if (talent?.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }

        public override bool CanHit(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return true;
            }

            if (this.Owner.Distance2D(targets.First()) < this.Radius)
            {
                return true;
            }

            // moar checks
            return false;
        }

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}