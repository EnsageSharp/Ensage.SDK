// <copyright file="skywrath_mage_ancient_seal.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_skywrath_mage
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class skywrath_mage_ancient_seal : RangedAbility, IHasTargetModifier, IHasDamageAmplifier
    {
        public skywrath_mage_ancient_seal(Ability ability)
            : base(ability)
        {
        }

        public DamageType AmplifierType { get; } = DamageType.Magical;

        public float DamageAmplification
        {
            get
            {
                var amplification = this.Ability.GetAbilitySpecialData("resist_debuff") / -100.0f;

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_skywrath_3);
                if (talent?.Level > 0)
                {
                    amplification += talent.GetAbilitySpecialData("value") / -100.0f;
                }

                return amplification;
            }
        }

        public string TargetModifierName { get; } = "modifier_skywrath_mage_ancient_seal";
    }
}