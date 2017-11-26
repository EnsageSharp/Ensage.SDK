// <copyright file="keeper_of_the_light_recall.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_keeper_of_the_light
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class keeper_of_the_light_recall : RangedAbility, IHasTargetModifier
    {
        public keeper_of_the_light_recall(Ability ability)
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

        public override bool IsReady
        {
            get
            {
                return base.IsReady && this.Ability.IsActivated;
            }
        }

        public string TargetModifierName { get; } = "modifier_keeper_of_the_light_recall";
    }
}