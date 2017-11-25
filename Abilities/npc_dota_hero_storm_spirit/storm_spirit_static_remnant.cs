// <copyright file="storm_spirit_static_remnant.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_storm_spirit
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class storm_spirit_static_remnant : ActiveAbility, IAreaOfEffectAbility
    {
        public storm_spirit_static_remnant(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("static_remnant_delay");
            }
        }

        public float DamageRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("static_remnant_damage_radius");
            }
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("static_remnant_radius");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "static_remnant_damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}