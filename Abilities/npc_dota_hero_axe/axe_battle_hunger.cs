// <copyright file="axe_battle_hunger.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_axe
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class axe_battle_hunger : RangedAbility, IHasDot, IHasModifier
    {
        public axe_battle_hunger(Ability ability)
            : base(ability)
        {
        }

        public float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public bool HasInitialDamage { get; } = false;

        public string ModifierName { get; } = "modifier_axe_battle_hunger_self";

        public float RawTickDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("damage_per_second");
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_axe);
                if (talent != null && talent.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }

        public string TargetModifierName { get; } = "modifier_axe_battle_hunger";

        public float TickRate { get; } = 1.0f;

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Ability.SpellAmplification();
            var reduction = 0.0f;
            if (targets.Any())
            {
                reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets) * (this.Duration / this.TickRate);
        }
    }
}