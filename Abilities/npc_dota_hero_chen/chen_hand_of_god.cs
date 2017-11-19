// <copyright file="chen_hand_of_god.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_chen
{
    public class chen_hand_of_god : ActiveAbility, IAreaOfEffectAbility
    {
        public chen_hand_of_god(Ability ability)
            : base(ability)
        {
        }

        public float Radius { get; } = float.MaxValue;
    }
}