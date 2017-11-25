// <copyright file="jakiro_dual_breath.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_jakiro
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class jakiro_dual_breath : ConeAbility, IHasDot
    {
        public jakiro_dual_breath(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                return this.Duration;
            }
        }

        public override float EndRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("end_radius");
            }
        }

        public bool HasInitialDamage { get; } = false;

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("start_radius");
            }
        }

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "burn_damage") * this.TickRate;
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("speed_fire");
            }
        }

        public string TargetModifierName { get; } = "modifier_jakiro_dual_breath_burn";

        public float TickRate { get; } = 0.5f;

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
            return this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate);
        }
    }
}