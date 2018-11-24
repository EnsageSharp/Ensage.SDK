// <copyright file="doom_bringer_devour.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_doom_bringer
{
    using Ensage.Common.Extensions;
    using Ensage.SDK.Abilities.Components;

    public class doom_bringer_devour : ToggleAbility, IHasModifier
    {
        public doom_bringer_devour(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_doom_bringer_devour";
    }
}