// <copyright file="batrider_flamebreak.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_batrider
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class batrider_flamebreak : CircleAbility, IHasDot
    {
        public batrider_flamebreak(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                var duration = this.Ability.GetAbilitySpecialData("damage_duration");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_batrider_3);
                if (talent?.Level > 0)
                {
                    duration += talent.GetAbilitySpecialData("value");
                }

                return duration;
            }
        }

        public bool HasInitialDamage { get; } = false;

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_per_second") * this.TickRate;
            }
        }

        public string TargetModifierName { get; } = "modifier_flamebreak_damage";

        public float TickRate { get; } = 1f;

        protected override string RadiusName { get; } = "explosion_radius";

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