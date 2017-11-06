// <copyright file="pudge_dismember.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_pudge
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class pudge_dismember : RangedAbility, IChannable, IHasDot
    {
        public pudge_dismember(Ability ability)
            : base(ability)
        {
        }

        public float ChannelDuration
        {
            get
            {
                var duration = this.Ability.GetAbilitySpecialData("ticks");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_pudge_3);
                if (talent?.Level > 0)
                {
                    duration += talent.GetAbilitySpecialData("value");
                }

                return duration;
            }
        }

        public float CreepChannelDuration
        {
            get
            {
                return this.ChannelDuration * 2;
            }
        }

        public float DamageDuration
        {
            get
            {
                return this.ChannelDuration;
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

        public float RawTickDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("dismember_damage");

                var hero = this.Owner as Hero;
                if (hero != null)
                {
                    var strDamage = this.Ability.GetAbilitySpecialData("strength_damage");
                    damage += strDamage * hero.TotalStrength;
                }

                return damage;
            }
        }

        public float RemainingCreepChannelDuration
        {
            get
            {
                if (!this.IsChanneling)
                {
                    return 0;
                }

                return this.CreepChannelDuration - this.Ability.ChannelTime;
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

        public string TargetModifierName { get; } = "modifier_pudge_dismember";

        public float TickRate
        {
            get
            {
                return 1.0f;
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets);
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
            // TODO: better .IsCreep method
            if (targets.Any() && !(targets.First() is Hero))
            {
                return this.GetTickDamage(targets) * (this.CreepChannelDuration / this.TickRate);
            }

            return this.GetTickDamage(targets) * (this.ChannelDuration / this.TickRate);
        }
    }
}