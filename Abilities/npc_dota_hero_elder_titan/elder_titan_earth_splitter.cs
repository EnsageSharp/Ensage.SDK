// <copyright file="elder_titan_earth_splitter.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_elder_titan
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class elder_titan_earth_splitter : LineAbility, IHasTargetModifier
    {
        public elder_titan_earth_splitter(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("crack_time");
            }
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("crack_width");
            }
        }

        public string TargetModifierName { get; } = "modifier_elder_titan_earth_splitter";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_pct") / 100f;
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                // ability deals half magic half phys damage 

                var halfDamage = damage * target.MaximumHealth * 0.5f;

                var physReduction = this.Ability.GetDamageReduction(target, DamageType.Physical);
                var magicReduction = this.Ability.GetDamageReduction(target, DamageType.Magical);

                totalDamage += DamageHelpers.GetSpellDamage(halfDamage, amplify, physReduction);
                totalDamage += DamageHelpers.GetSpellDamage(halfDamage, amplify, magicReduction);
            }

            return totalDamage;
        }
    }
}