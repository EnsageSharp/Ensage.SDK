// <copyright file="snapfire_firesnap_cookie.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

using Ensage.SDK.Abilities.Components;

namespace Ensage.SDK.Abilities.npc_dota_hero_snapfire
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class snapfire_firesnap_cookie : RangedAbility, IHasHealthRestore
    {
        public snapfire_firesnap_cookie(Ability ability)
            : base(ability)
        {
        }

        // Projectile speed
        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("projectile_speed");
            }
        }

        // jump duration
        public override float Duration
        {
            get
            {
                return Ability.GetAbilitySpecialData("jump_duration");
            }
        }

        public float JumpDistance
        {
            get
            {
                return Ability.GetAbilitySpecialData("jump_horizontal_distance");
            }
        }

        public float ImpactRadius
        {
            get
            {
                return Ability.GetAbilitySpecialData("impact_radius");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("impact_damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        public float TotalHealthRestore
        {
            get
            {
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_snapfire_5);
                return talent?.Level > 0 ? talent.GetAbilitySpecialData("value") : 0;
            }
        }
    }
}