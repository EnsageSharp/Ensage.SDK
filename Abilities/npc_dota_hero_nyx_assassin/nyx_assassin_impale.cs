// <copyright file="nyx_assassin_impale.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_nyx_assassin
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class nyx_assassin_impale : LineAbility, IHasTargetModifier
    {
        public nyx_assassin_impale(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("width");
            }
        }

        public string TargetModifierName { get; } = "modifier_nyx_assassin_impale";

        protected override float BaseCastRange
        {
            get
            {
                var modifier = this.Owner.GetModifierByName("modifier_nyx_assassin_burrow");
                if (modifier != null)
                {
                    var ability = this.Owner.GetAbilityById(AbilityId.nyx_assassin_burrow);
                    return ability.GetAbilitySpecialData("impale_burrow_range_tooltip");
                }

                return this.Ability.CastRange;
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "impale_damage");
            }
        }
    }
}