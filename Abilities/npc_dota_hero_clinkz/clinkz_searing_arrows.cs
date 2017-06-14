// <copyright file="clinkz_searing_arrows.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_clinkz
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class clinkz_searing_arrows : OrbAbility
    {
        public clinkz_searing_arrows(Ability ability)
            : base(ability)
        {
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = base.GetDamage(targets);
            var amp = this.Owner.GetSpellAmplification();
            var damageBonus = this.Ability.GetAbilitySpecialData("damage_bonus");

            var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_clinkz_1);
            if (talent != null && talent.Level > 0)
            {
                damageBonus += talent.GetAbilitySpecialData("value");
            }

            var reduction = 0.0f;
            if (targets.Any())
            {
                reduction = this.Ability.GetDamageReduction(targets.First());
            }

            damage += DamageHelpers.GetSpellDamage(damageBonus, amp, reduction);

            return damage;
        }
    }
}