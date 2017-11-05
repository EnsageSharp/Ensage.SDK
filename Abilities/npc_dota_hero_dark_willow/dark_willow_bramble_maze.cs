// <copyright file="dark_willow_bramble_maze.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_dark_willow
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class dark_willow_bramble_maze : CircleAbility, IHasTargetModifier
    {
        public dark_willow_bramble_maze(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return (this.Ability.GetAbilitySpecialData("initial_creation_delay") + this.Ability.GetAbilitySpecialData("latch_creation_delay")) * 1000;
            }
        }

        public string TargetModifierName { get; } = "modifier_dark_willow_bramble_maze";

        protected override string RadiusName { get; } = "placement_range";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("latch_damage");
            }
        }
    }
}