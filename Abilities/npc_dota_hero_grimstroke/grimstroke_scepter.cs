// <copyright file="grimstroke_scepter.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_grimstroke
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Abilities.Components;

    public class grimstroke_scepter : RangedAbility
    {
        // Dark Portrait ability that comes with Aghanim's Scepter
        public grimstroke_scepter(Ability ability)
            : base(ability)
        {
        }

        public override float Duration
        {
            get
            {
                return Ability.GetAbilitySpecialData("illusion_duration");
            }
        }
    }
}