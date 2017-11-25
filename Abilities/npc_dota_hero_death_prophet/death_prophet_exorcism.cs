// <copyright file="death_prophet_exorcism.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_death_prophet
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class death_prophet_exorcism : ActiveAbility, IAreaOfEffectAbility, IHasModifier
    {
        public death_prophet_exorcism(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_death_prophet_exorcism";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("spirit_speed");
            }
        }
    }
}