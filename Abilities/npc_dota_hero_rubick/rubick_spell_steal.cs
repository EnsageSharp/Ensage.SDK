// <copyright file="rubick_spell_steal.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_rubick
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class rubick_spell_steal : RangedAbility, IHasModifier
    {
        public rubick_spell_steal(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_rubick_spell_steal";

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("projectile_speed");
            }
        }
    }
}