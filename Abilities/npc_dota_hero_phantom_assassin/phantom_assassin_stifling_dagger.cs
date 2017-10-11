// <copyright file="phantom_assassin_stifling_dagger.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_phantom_assassin
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class phantom_assassin_stifling_dagger : RangedAbility, IHasTargetModifier, IHasModifier
    {
        public phantom_assassin_stifling_dagger(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_phantom_assassin_stiflingdagger_caster";

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("dagger_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_phantom_assassin_stiflingdagger";

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("base_damage");

                var bonus = this.Ability.GetAbilitySpecialData("attack_factor_tooltip") / 100.0f; // 70
                damage += bonus * (this.Owner.MinimumDamage + this.Owner.BonusDamage);

                return damage;
            }
        }
    }
}