// <copyright file="obsidian_destroyer_arcane_orb.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_obsidian_destroyer
{
    using System.Linq;

    using Ensage.SDK.Extensions;

    public class obsidian_destroyer_arcane_orb : OrbAbility
    {
        public obsidian_destroyer_arcane_orb(Ability ability)
            : base(ability)
        {
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = base.GetDamage(targets);

            var manaPoolDamage = this.Ability.GetAbilitySpecialData("mana_pool_damage_pct") / 100.0f; // TODO: Spell Amp?
            damage += this.Owner.Mana * manaPoolDamage;

            if (targets.Any())
            {
                var target = targets.First();
                if (target.IsIllusion || target.IsSummoned)
                {
                    damage += this.Ability.GetAbilitySpecialData("illusion_damage");
                }
            }

            return damage;
        }
    }
}