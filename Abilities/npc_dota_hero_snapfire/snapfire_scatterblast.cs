// <copyright file="snapfire_scatterblast.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_snapfire
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class snapfire_scatterblast : ConeAbility
    {
        public snapfire_scatterblast(Ability ability)
            : base(ability)
        {
        }

        public override float EndRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("blast_width_end");
            }
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("blast_width_initial");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("blast_speed");
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
                totalDamage += Owner.Distance2D(target) <= 450 ? DamageHelpers.GetSpellDamage(damage, amplify, reduction) * 1.5f
                    : DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}