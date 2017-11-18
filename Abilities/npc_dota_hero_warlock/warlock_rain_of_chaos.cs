// <copyright file="warlock_rain_of_chaos.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_warlock
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class warlock_rain_of_chaos : CircleAbility, IHasTargetModifier, IHasTargetModifierTexture
    {
        public warlock_rain_of_chaos(Ability ability)
            : base(ability)
        {
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("aoe");
            }
        }

        public string TargetModifierName { get; } = "modifier_warlock_golem_permanent_immolation_debuff";

        public string[] TargetModifierTextureName { get; } = { "warlock_rain_of_chaos" };
    }
}