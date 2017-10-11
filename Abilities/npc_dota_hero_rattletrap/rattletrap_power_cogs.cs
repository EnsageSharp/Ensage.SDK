// <copyright file="rattletrap_power_cogs.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_rattletrap
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class rattletrap_power_cogs : ActiveAbility, IAreaOfEffectAbility, IHasTargetModifier
    {
        public rattletrap_power_cogs(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("cogs_radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_rattletrap_cog_push";
    }
}