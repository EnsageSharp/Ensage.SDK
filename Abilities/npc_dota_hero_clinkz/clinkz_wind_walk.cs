// <copyright file="clinkz_wind_walk.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_clinkz
{
    public class clinkz_wind_walk : ActiveAbility, IHasModifier
    {
        public clinkz_wind_walk(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_clinkz_wind_walk";
    }
}