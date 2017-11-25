// <copyright file="chen_holy_persuasion.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_chen
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class chen_holy_persuasion : RangedAbility, IHasTargetModifier
    {
        public chen_holy_persuasion(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("teleport_delay");
            }
        }

        public string TargetModifierName { get; } = "modifier_chen_test_of_faith_teleport";
    }
}