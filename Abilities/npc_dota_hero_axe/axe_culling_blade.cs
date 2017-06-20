// <copyright file="axe_culling_blade.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_axe
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class axe_culling_blade : RangedAbility, IHasModifier
    {
        public axe_culling_blade(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_axe_culling_blade_boost";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }

        /// <summary>
        ///     Gets the damage of the ability. If the target is below the threshold to instant kill it, it will return
        ///     float.MaxValue.
        /// </summary>
        /// <param name="targets"></param>
        /// <returns></returns>
        public override float GetDamage(params Unit[] targets)
        {
            var reduction = 0.0f;
            if (targets.Any())
            {
                var target = targets.First();
                var threshold = this.Ability.GetAbilitySpecialData("kill_threshold");

                var healthRegen = target.HealthRegeneration * this.GetCastDelay(target) / 1000.0f * 2.0f;
                if (target.Health + healthRegen < threshold)
                {
                    return float.MaxValue;
                }

                reduction = this.Ability.GetDamageReduction(target);
            }

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }
    }
}