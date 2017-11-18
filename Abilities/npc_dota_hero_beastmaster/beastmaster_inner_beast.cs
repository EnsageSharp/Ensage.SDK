// <copyright file="beastmaster_inner_beast.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_beastmaster
{
    using Ensage.SDK.Extensions;

    public class beastmaster_inner_beast : AuraAbility
    {
        public beastmaster_inner_beast(Ability ability)
            : base(ability)
        {
        }

        public override string AuraModifierName { get; } = "modifier_beastmaster_inner_beast";

        public override float AuraRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }
    }
}