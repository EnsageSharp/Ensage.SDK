// <copyright file="obsidian_destroyer_arcane_orb.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_obsidian_destroyer
{
    using System;
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class obsidian_destroyer_arcane_orb : OrbAbility
    {
        public obsidian_destroyer_arcane_orb(Ability ability)
            : base(ability)
        {
        }


        public override float GetDamage(params Unit[] targets)
        {
            var damage = base.GetDamage(targets);
            var spellAmp = this.Owner.GetSpellAmplification();

            var manaPoolDamage = this.Ability.GetAbilitySpecialData("mana_pool_damage_pct") / 100.0f;
            var bonusDamage = Math.Max(this.Owner.Mana - this.ManaCost, 0) * manaPoolDamage;

            var reduction = 0.0f;
            if (targets.Any())
            {
                var target = targets.First();
                reduction = this.Ability.GetDamageReduction(target, this.DamageType);
            }

            damage += DamageHelpers.GetSpellDamage(bonusDamage, spellAmp, reduction);
            return damage;
        }
    }
}