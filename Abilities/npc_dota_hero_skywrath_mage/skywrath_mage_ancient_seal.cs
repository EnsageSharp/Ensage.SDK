// <copyright file="skywrath_mage_ancient_seal.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_skywrath_mage
{
    public class skywrath_mage_ancient_seal : RangedAbility, IHasTargetModifier
    {
        public skywrath_mage_ancient_seal(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_skywrath_mage_ancient_seal";
    }
}