// <copyright file="skywrath_mage_ancient_seal.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_skywrath_mage
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    using PlaySharp.Toolkit.Helper.Annotations;

    [PublicAPI]
    public class skywrath_mage_ancient_seal : RangedAbility, IHasTargetModifier, IHasDamageAmplifier
    {
        public skywrath_mage_ancient_seal(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_skywrath_mage_ancient_seal";

        public DamageType AmplifierType { get; } = DamageType.Magical;

        public float Value
        {
            get
            {
                return -this.Ability.GetAbilitySpecialData("resist_debuff") / 100.0f;
            }
        }
    }
}