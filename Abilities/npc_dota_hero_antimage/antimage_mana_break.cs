// <copyright file="antimage_mana_break.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_antimage
{
    using System;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class antimage_mana_break : PassiveAbility
    {
        public antimage_mana_break(Ability ability)
            : base(ability)
        {
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("mana_per_hit");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var manaBurnMultiplier = this.Ability.GetAbilitySpecialData("damage_per_burn");
            if (!targets.Any())
            {
                return this.Owner.MinimumDamage + this.Owner.BonusDamage + (this.RawDamage * manaBurnMultiplier);
            }

            var reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            var manaBurnDamage = Math.Min(this.RawDamage, targets.First().Mana) * manaBurnMultiplier;

            return this.Owner.GetAttackDamage(targets.First()) + DamageHelpers.GetSpellDamage(manaBurnDamage, 0, reduction);
        }
    }
}