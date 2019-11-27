// <copyright file="kunkka_torrent_storm.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.SDK.Extensions;

namespace Ensage.SDK.Abilities.npc_dota_hero_kunkka
{
    using Ensage.SDK.Abilities.Components;

    public class kunkka_torrent_storm : ActiveAbility
    {
        public kunkka_torrent_storm(Ability ability)
            : base(ability)
        {
        }

        public override float Duration
        {
            get
            {
                return Ability.GetAbilitySpecialData("torrent_duration");
            }
        }

        public float Radius
        {
            get
            {
                return Ability.GetAbilitySpecialData("torrent_max_distance");
            }
        }
    }
}