// <copyright file="pudge_flesh_heap.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_pudge
{
    public class pudge_flesh_heap : PassiveAbility, IHasModifier
    {
        public pudge_flesh_heap(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_pudge_flesh_heap";
    }
}