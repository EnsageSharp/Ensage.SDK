// <copyright file="puck_waning_rift.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_puck
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class puck_waning_rift : ActiveAbility, IAreaOfEffectAbility, IHasTargetModifierTexture
    {
        public puck_waning_rift(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public string[] TargetModifierTextureName { get; } = { "puck_waning_rift" };

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