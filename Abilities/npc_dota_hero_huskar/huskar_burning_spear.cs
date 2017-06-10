// <copyright file="huskar_burning_spear.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_obsidian_destroyer
{

    using Ensage.Common.Extensions;

    using Ensage.SDK.Extensions;

    using Ensage.SDK.Helpers;

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

            var spearDamage = this.GetRawDamage() * (1.0f + this.Owner.GetSpellAmplification()) * 8;

            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target);
                damage += spearDamage * (1.0f - reduction);

                if (talent != null)
                {
                    var talentDamage = talent.GetAbilitySpecialData("value");
                    damage += (talentDamage * (1.0f - reduction)) * 8;
                }
            }

            return damage;
        }
    }
}

