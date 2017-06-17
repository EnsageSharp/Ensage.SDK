// <copyright file="clinkz_strafe.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_clinkz
{
    public class clinkz_strafe : ActiveAbility, IHasModifier
    {
        public clinkz_strafe(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_clinkz_strafe";
    }
}