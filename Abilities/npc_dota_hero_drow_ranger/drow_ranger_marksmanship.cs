// <copyright file="drow_ranger_marksmanship.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_drow_ranger
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class drow_ranger_marksmanship : PassiveAbility, IHasModifier, IAreaOfEffectAbility
    {
        public drow_ranger_marksmanship(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_drow_ranger_marksmanship";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }
    }
}