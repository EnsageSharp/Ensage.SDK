// <copyright file="item_meteor_hammer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class item_meteor_hammer : RangedAbility, IAreaOfEffectAbility, IChannable, IHasDot
    {
        public item_meteor_hammer(Item item)
            : base(item)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("land_time");
            }
        }

        public float ChannelDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("max_duration");
            }
        }

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("burn_duration");
            }
        }

        public override DamageType DamageType
        {
            get
            {
                return DamageType.Magical;
            }
        }

        public bool HasInitialDamage { get; } = true;

        public bool IsChanneling
        {
            get
            {
                return this.Ability.IsChanneling;
            }
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("impact_radius");
            }
        }

        public float RawTickDamage
        {
            get
            {
                // burn_dps_buildings for buildings
                return this.Ability.GetAbilitySpecialData("burn_dps_units");
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

                return this.ChannelDuration - this.Ability.ChannelTime;
            }
        }

        public string TargetModifierName { get; } = "modifier_meteor_hammer_burn";

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("burn_interval");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("impact_damage_units");
            }
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Ability.SpellAmplification();
            var reduction = 0.0f;
            if (targets.Any())
            {
                reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetDamage(targets) + (this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate));
        }
    }
}