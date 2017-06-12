// <copyright file="obsidian_destroyer_arcane_orb.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_obsidian_destroyer
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;


    public class obsidian_destroyer_arcane_orb : OrbAbility, IHasModifier, IHasTargetModifier
    {
        public obsidian_destroyer_arcane_orb(Ability ability)
            : base(ability)
        {
        }

        /// <summary>
        ///     Gets the name of the modifier, which holds the total count of stacks acquired.
        /// </summary>
        public string ModifierCounterName { get; } = "modifier_obsidian_destroyer_astral_imprisonment_buff_counter";

        /// <summary>
        ///     Gets the name of the modifier for each stack instance of the buff.
        /// </summary>
        public string ModifierName { get; } = "modifier_obsidian_destroyer_astral_imprisonment_buff";

        public override float Speed
        {
            get
            {
                return this.Owner.ProjectileSpeed();
            }
        }

        /// <summary>
        ///     Gets the name of the modifier for enemy heroes, which holds the total count of stacks acquired.
        /// </summary>
        public string TargetModifierCounterName { get; } = "modifier_obsidian_destroyer_astral_imprisonment_debuff_counter";

        /// <summary>
        ///     Gets the name of the modifier for enemy heroes, for each stack instance of the debuff.
        /// </summary>
        public string TargetModifierName { get; } = "modifier_obsidian_destroyer_astral_imprisonment_debuff";

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