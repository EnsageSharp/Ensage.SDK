// <copyright file="lion_voodoo.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_lion
{
    using Ensage.SDK.Abilities.Components;

    public class lion_voodoo : RangedAbility, IHasTargetModifier
    {
        // TODO lvl 25 perk
        public lion_voodoo(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_lion_voodoo";
    }
}