// <copyright file="obsidian_destroyer_arcane_orb.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_obsidian_destroyer
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class obsidian_destroyer_arcane_orb : OrbAbility
    {
        public obsidian_destroyer_arcane_orb(Ability abiltity)
            : base(abiltity)
        {
        }

        public override float Speed
        {
            get
            {
                return this.Owner.ProjectileSpeed();
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = base.GetDamage(targets);

            var manaPoolDamage = this.Ability.GetAbilitySpecialData("mana_pool_damage_pct") / 100.0f;
            damage += DamageHelpers.GetSpellDamage(this.Owner.Mana * manaPoolDamage, this.Owner.GetSpellAmplification());

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