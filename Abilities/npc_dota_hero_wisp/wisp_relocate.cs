// <copyright file="wisp_relocate.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_wisp
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class wisp_relocate : RangedAbility, IHasModifier
    {
        public wisp_relocate(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("cast_delay");
            }
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("return_time");
            }
        }

        public string ModifierName { get; } = "modifier_teleporting";
    }
}