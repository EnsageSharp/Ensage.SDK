// <copyright file="night_stalker_hunter_in_the_night.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_night_stalker
{
    using Ensage.SDK.Abilities.Components;

    public class night_stalker_hunter_in_the_night : ActiveAbility, IHasModifier
    {
        public night_stalker_hunter_in_the_night(Ability ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return Game.IsNight && base.CanBeCasted;
            }
        }

        public string ModifierName { get; } = "modifier_night_stalker_hunter_in_the_night_flight";
    }
}