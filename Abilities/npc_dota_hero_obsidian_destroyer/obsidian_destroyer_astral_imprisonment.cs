// <copyright file="obsidian_destroyer_astral_imprisonment.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_obsidian_destroyer
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class obsidian_destroyer_astral_imprisonment : AreaOfEffectAbility, IHasTargetModifier
    {
        public obsidian_destroyer_astral_imprisonment(Ability ability)
            : base(ability)
        {
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_obsidian_destroyer_astral_imprisonment_prison";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }
    }
}