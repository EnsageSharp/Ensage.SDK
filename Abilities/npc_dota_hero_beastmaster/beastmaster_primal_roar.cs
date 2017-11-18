// <copyright file="beastmaster_primal_roar.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_beastmaster
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class beastmaster_primal_roar : RangedAbility, IHasTargetModifierTexture
    {
        public beastmaster_primal_roar(Ability ability)
            : base(ability)
        {
        }

        public string[] TargetModifierTextureName { get; } = { "beastmaster_primal_roar" };

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }
    }
}