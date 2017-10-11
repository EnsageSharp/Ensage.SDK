// <copyright file="drow_ranger_trueshot.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_drow_ranger
{
    using Ensage.SDK.Abilities.Components;

    public class drow_ranger_trueshot : ActiveAbility, IHasModifier
    {
        public drow_ranger_trueshot(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_drow_ranger_trueshot_global";
    }
}