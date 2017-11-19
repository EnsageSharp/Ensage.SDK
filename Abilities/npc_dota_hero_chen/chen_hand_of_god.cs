// <copyright file="chen_hand_of_god.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_chen
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class chen_hand_of_god : ActiveAbility, IAreaOfEffectAbility, IHasHealthRestore
    {
        public chen_hand_of_god(Ability ability)
            : base(ability)
        {
        }

        public float Radius { get; } = float.MaxValue;

        public float TotalHealthRestore
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "heal_amount");
            }
        }
    }
}