// <copyright file="huskar_burning_spear.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using System.Linq;

namespace Ensage.SDK.Abilities.npc_dota_hero_huskar
{

    using Ensage.SDK.Extensions;

    using Ensage.SDK.Helpers;

    using Ensage.Common.Extensions;

    public class huskar_burning_spear : OrbAbility, IHasTargetModifier
    {
        public huskar_burning_spear(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName => "modifier_huskar_burning_spear_counter";

        public override float GetDamage(params Unit[] targets)
        {
            var damage = base.GetDamage(targets);

            var talent = this.Owner.FindSpell("special_bonus_unique_huskar_2");

            // var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_huskar_2); // missing AbilityId

            var spearDamage = this.GetRawDamage() * (1.0f + this.Owner.GetSpellAmplification());

            var target = targets.First();

            if (targets.Any())
            {
                return damage;
            }

            var reduction = this.Ability.GetDamageReduction(target);

            if (talent != null && talent.Level > 0)
            {
                 var talentDamage = talent.GetAbilitySpecialData("value");
                 damage += ((talentDamage + spearDamage) * (1.0f - reduction)) * 8;
            }
           else
            {
                 damage += (spearDamage * (1.0f - reduction)) * 8;
            }

         return damage;

        }
    }
}

