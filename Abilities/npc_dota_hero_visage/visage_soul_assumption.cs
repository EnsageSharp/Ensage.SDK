// <copyright file="visage_soul_assumption.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_visage
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class visage_soul_assumption : RangedAbility, IHasModifier
    {
        public visage_soul_assumption(Ability ability)
            : base(ability)
        {
        }

        public float Charges
        {
            get
            {
                var modifier = this.Owner.GetModifierByName(this.ModifierName);
                return modifier?.StackCount ?? 0;
            }
        }

        public bool MaxCharges
        {
            get
            {
                return this.Charges == this.Ability.GetAbilitySpecialData("stack_limit");
            }
        }

        public string ModifierName { get; } = "modifier_visage_soul_assumption";

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("soul_base_damage");
                var bonus = this.Ability.GetAbilitySpecialData("soul_charge_damage");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_visage_4);
                if (talent?.Level > 0)
                {
                    bonus += talent.GetAbilitySpecialData("value");
                }

                damage += this.Charges * bonus;

                return damage;
            }
        }
    }
}