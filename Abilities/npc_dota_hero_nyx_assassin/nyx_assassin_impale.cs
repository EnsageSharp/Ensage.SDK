// <copyright file="nyx_assassin_impale.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_nyx_assassin
{
    using Ensage.SDK.Extensions;

    public class nyx_assassin_impale : LineAbility, IHasTargetModifier
    {
        public nyx_assassin_impale(Ability ability)
            : base(ability)
        {
        }
        
        public string TargetModifierName { get; } = "modifier_nyx_assassin_impale";

        protected override string RadiusName { get; } = "width";

        protected override string SpeedName { get; } = "speed";

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
                var damage = this.Ability.GetAbilitySpecialData("#AbilityDamage");

                var damageTalent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_nyx_2);
                if (damageTalent != null && damageTalent.Level > 0)
                {
                    damage += damageTalent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }
    }
}