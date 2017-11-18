// <copyright file="ember_spirit_sleight_of_fist.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_ember_spirit
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class ember_spirit_sleight_of_fist : CircleAbility, IHasModifier, IHasTargetModifier
    {
        public ember_spirit_sleight_of_fist(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_ember_spirit_sleight_of_fist_caster";

        public string TargetModifierName { get; } = "modifier_ember_spirit_sleight_of_fist_marker";

        protected override float RawDamage
        {
            get
            {
                return this.Owner.MinimumDamage + this.Owner.BonusDamage;
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return this.RawDamage;
            }

            var creepPenalty = this.Ability.GetAbilitySpecialData("creep_damage_penalty") / -100;
            var heroBonus = this.Ability.GetAbilitySpecialData("bonus_hero_damage");

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var targetDamage = this.Owner.GetAttackDamage(target, true);

                if (target is Creep)
                {
                    totalDamage += targetDamage * creepPenalty;
                }
                else
                {
                    var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                    totalDamage += DamageHelpers.GetSpellDamage(heroBonus, 0, reduction) + targetDamage;
                }
            }

            return totalDamage;
        }
    }
}