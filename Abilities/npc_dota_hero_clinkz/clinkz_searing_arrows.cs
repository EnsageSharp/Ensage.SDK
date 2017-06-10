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

            var arrowDamage = this.Ability.GetAbilitySpecialData("damage_bonus");
            damage += arrowDamage;
            var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_clinkz_1);

            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target);

                if (talent != null)
                {
                    var talentDamage = talent.GetAbilitySpecialData("value");
                    damage += (arrowDamage + talentDamage) * (1.0f - reduction);
                }
                else
                {
                    damage += arrowDamage * (1.0f - reduction);
                }
            }

            return damage;
        }
    }
}