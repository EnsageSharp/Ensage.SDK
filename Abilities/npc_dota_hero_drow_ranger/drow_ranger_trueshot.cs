// <copyright file="drow_ranger_trueshot.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_drow_ranger
{
    public class drow_ranger_trueshot : ActiveAbility, IHasModifier
    {
        public drow_ranger_trueshot(Ability abiltity)
            : base(abiltity)
        {
        }

        public string ModifierName { get; } = "modifier_drow_ranger_trueshot_global";
    }
}