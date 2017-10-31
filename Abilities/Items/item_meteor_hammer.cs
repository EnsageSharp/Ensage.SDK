using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ensage.SDK.Extensions;
using Ensage.SDK.Helpers;

namespace Ensage.SDK.Abilities.Items
{
    public class item_meteor_hammer : RangedAbility, IAreaOfEffectAbility, IChannable, IHasDot
    {
        public item_meteor_hammer(Item item)
            : base(item)
        {
        }

        public bool HasInitialDamage { get; } = true;
        public string TargetModifierName { get; } = "modifier_meteor_hammer_burn"; //todo: get correct modifier

        public float Radius
        {
            get
            {
               return Ability.GetAbilitySpecialData("impact_radius");
            }
        }

        public float Duration
        {
            get
            {
                return Ability.GetAbilitySpecialData("burn_duration");
            }
        }

        public bool IsChanneling
        {
            get
            {
                return this.Ability.IsChanneling;
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

                return (Ability.GetAbilitySpecialData("max_duration") + Ability.GetAbilitySpecialData("land_time")) - this.Ability.ChannelTime;
            }
        }

        public float TickRate
        {
            get
            {
                return 1.0f;
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
            return this.GetTickDamage(targets) * (this.Duration / this.TickRate);
        }

    }
}
