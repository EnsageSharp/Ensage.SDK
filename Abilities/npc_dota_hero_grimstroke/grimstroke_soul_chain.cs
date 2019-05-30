// <copyright file="grimstroke_soul_chain.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_grimstroke
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Abilities.Components;

    public class grimstroke_soul_chain : RangedAbility, IHasTargetModifier
    {
        public grimstroke_soul_chain(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_grimstroke_soul_chain";

        public override float CastRange
        {
            get
            {
                return Ability.GetAbilitySpecialData("cast_range_tooltip");
            }
        }

        public override float Duration
        {
            get
            {
                return Ability.GetAbilitySpecialData("chain_duration");
            }
        }
    }
}