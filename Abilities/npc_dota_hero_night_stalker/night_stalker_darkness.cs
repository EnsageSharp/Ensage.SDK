// <copyright file="night_stalker_darkness.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_night_stalker
{
    using Ensage.SDK.Abilities.Components;

    // They didn't change the internal name again.
    public class night_stalker_darkness : ActiveAbility, IHasModifier
    {
        public night_stalker_darkness(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_night_stalker_darkness";
    }
}