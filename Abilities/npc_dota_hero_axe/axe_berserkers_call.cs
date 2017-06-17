// <copyright file="axe_berserkers_call.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_axe
{
    using Ensage.SDK.Extensions;

    public class axe_berserkers_call : ActiveAbility, IAreaOfEffectAbility, IHasTargetModifier, IHasModifier
    {
        public axe_berserkers_call(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_axe_berserkers_call_armor";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_axe_berserkers_call";
    }
}