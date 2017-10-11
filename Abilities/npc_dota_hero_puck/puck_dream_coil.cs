// <copyright file="puck_dream_coil.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_puck
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class puck_dream_coil : CircleAbility, IAreaOfEffectAbility, IHasTargetModifier
    {
        public puck_dream_coil(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_puck_coiled";

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("coil_radius");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("coil_init_damage_tooltip");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}