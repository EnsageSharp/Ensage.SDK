// <copyright file="beastmaster_wild_axes.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_beastmaster
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class beastmaster_wild_axes : LineAbility, IHasModifier, IHasTargetModifier
    {
        public beastmaster_wild_axes(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_beastmaster_wild_axes";

        public override float Speed { get; } = 1200f; // no special data

        public string TargetModifierName { get; } = "modifier_beastmaster_axe_stack_counter";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "axe_damage") * 2;
            }
        }
    }
}