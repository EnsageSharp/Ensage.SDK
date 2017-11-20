// <copyright file="dark_seer_surge.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_dark_seer
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class dark_seer_surge : RangedAbility, IAreaOfEffectAbility, IHasModifier
    {
        public dark_seer_surge(Ability ability)
            : base(ability)
        {
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public string ModifierName { get; } = "modifier_dark_seer_surge";

        public float Radius
        {
            get
            {
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_dark_seer_3);
                if (talent?.Level > 0)
                {
                    return talent.GetAbilitySpecialData("value");
                }

                return 0;
            }
        }
    }
}