// <copyright file="silencer_last_word.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_silencer
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class silencer_last_word : RangedAbility, IHasTargetModifier
    {
        public silencer_last_word(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("debuff_duration");
            }
        }

        public float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public string TargetModifierName { get; } = "modifier_silencer_last_word_disarm";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }
    }
}