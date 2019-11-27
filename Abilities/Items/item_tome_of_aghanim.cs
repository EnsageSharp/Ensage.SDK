// <copyright file="item_tome_of_aghanim.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_tome_of_aghanim : RangedAbility, IHasTargetModifier
    {
        public item_tome_of_aghanim(Item item)
            : base(item)
        {
        }

        public override float Duration
        {
            get
            {
                return Ability.GetAbilitySpecialData("duration_minutes") * 60f;
            }
        }

        public string TargetModifierName { get; } = "modifier_tome_of_aghanim";
    }
}