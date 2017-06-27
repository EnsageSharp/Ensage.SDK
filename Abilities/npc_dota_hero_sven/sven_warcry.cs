// <copyright file="sven_warcry.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_sven
{
    using Ensage.SDK.Extensions;

    public class sven_warcry : ActiveAbility, IAreaOfEffectAbility, IHasModifier
    {
        public sven_warcry(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_sven_warcry";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("warcry_radius");
            }
        }
    }
}