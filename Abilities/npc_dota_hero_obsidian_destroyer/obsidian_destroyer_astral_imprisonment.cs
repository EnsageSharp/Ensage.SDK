// <copyright file="obsidian_destroyer_astral_imprisonment.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using System.Linq;

namespace Ensage.SDK.Abilities.npc_dota_hero_obsidian_destroyer
{

    using Ensage.SDK.Extensions;

    using Ensage.SDK.Helpers;


    public class obsidian_destroyer_astral_imprisonment : RangedAbility, IHasTargetModifier, IHasModifier
    {
        public obsidian_destroyer_astral_imprisonment(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName => "modifier_obsidian_destroyer_astral_imprisonment_buff";
        public string TargetModifierName => "modifier_obsidian_destroyer_astral_imprisonment_debuff";

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.GetRawDamage();
            var amplify = this.Owner.GetSpellAmplification();
            var target = targets.First();
            var reduction = this.Ability.GetDamageReduction(target);

            if (!targets.Any())
            {
                return damage;
            }

            totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            
            return totalDamage;
        }
    }
}
