// <copyright file="clinkz_searing_arrows.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_clinkz
{
    using System;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class clinkz_searing_arrows : OrbAbility
    {
        public clinkz_searing_arrows(Ability ability)
            : base(ability)
        {
        }

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("damage_bonus");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_clinkz_1);
                if (talent?.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return base.GetDamage() + this.RawDamage;
            }

            var reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            var amplify = this.Ability.SpellAmplification();

            return base.GetDamage(targets) + DamageHelpers.GetSpellDamage(this.RawDamage, amplify, reduction);
        }
    }
}