// <copyright file="gyrocopter_call_down.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_gyrocopter
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class gyrocopter_call_down : CircleAbility, IHasTargetModifier
    {
        public gyrocopter_call_down(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay { get; } = 2.0f;

        public override float CastRange
        {
            get
            {
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_gyrocopter_5);
                if (talent?.Level > 0)
                {
                    return float.MaxValue;
                }

                return base.CastRange;
            }
        }

        public string TargetModifierName { get; } = "modifier_gyrocopter_call_down_slow";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_first");
            }
        }
    }
}