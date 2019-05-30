// <copyright file="grimstroke_spirit_walk.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_grimstroke
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Abilities.Components;

    public class grimstroke_spirit_walk : RangedAbility, IHasModifier, IAreaOfEffectAbility
    {
        public grimstroke_spirit_walk(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public string ModifierName { get; } = "modifier_grimstroke_spirit_walk";

        public override float CastRange
        {
            get
            {
               return Ability.GetAbilitySpecialData("cast_range_tooltip");
            }
        }

        public float Radius
        {
            get
            {
                return Ability.GetAbilitySpecialDataWithTalent(Owner, "radius");
            }
        }
    }
}
