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

        public float CreepDuration
        {
            get
            {
                return this.Duration * 2;
            }
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

        public float RemainingCreepDuration
        {
            get
            {
                if (!this.IsChanneling)
                {
                    return 0;
                }

                return this.CreepDuration - this.Ability.ChannelTime;
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
                return this.GetTickDamage(targets) * (this.CreepDuration / this.TickRate);
            }

            return this.GetTickDamage(targets) * (this.Duration / this.TickRate);
        }
    }
}