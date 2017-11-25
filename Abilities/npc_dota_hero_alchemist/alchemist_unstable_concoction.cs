// <copyright file="alchemist_unstable_concoction.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_alchemist
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class alchemist_unstable_concoction : RangedAbility, IHasModifier, IHasTargetModifierTexture, IAreaOfEffectAbility
    {
        public alchemist_unstable_concoction(Ability ability)
            : base(ability)
        {
            var throwAbility = this.Owner.GetAbilityById(AbilityId.alchemist_unstable_concoction_throw);
            this.ThrowAbility = new alchemist_unstable_concoction_throw(throwAbility);
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("brew_time");
            }
        }

        public float ExplosionDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("brew_explosion");
            }
        }

        public override bool IsReady
        {
            get
            {
                return base.IsReady && !this.Ability.IsHidden;
            }
        }

        public string ModifierName { get; } = "modifier_alchemist_unstable_concoction";

        public float Radius
        {
            get
            {
                return this.ThrowAbility.Radius;
            }
        }

        public override float Speed
        {
            get
            {
                return this.ThrowAbility.Speed;
            }
        }

        public string[] TargetModifierTextureName
        {
            get
            {
                return this.ThrowAbility.TargetModifierTextureName;
            }
        }

        public alchemist_unstable_concoction_throw ThrowAbility { get; }

        public float GetDamage(float concotionDuration, params Unit[] targets)
        {
            var minDamage = this.Ability.GetAbilitySpecialData("min_damage");
            var maxDamage = this.Ability.GetAbilitySpecialData("max_damage");

            var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_alchemist_2);
            if (talent != null && talent.Level > 0)
            {
                var dmgValue = talent.GetAbilitySpecialData("value");
                minDamage += dmgValue;
                maxDamage += dmgValue;
            }

            var percentage = concotionDuration / this.Duration;
            var damage = minDamage + ((maxDamage - minDamage) * percentage);
            var amp = this.Owner.GetSpellAmplification();
            var reduction = 0.0f;
            if (targets.Any())
            {
                reduction = this.Ability.GetDamageReduction(targets.First(), this.Ability.DamageType);
            }

            return DamageHelpers.GetSpellDamage(damage, amp, reduction);
        }

        public override float GetDamage(params Unit[] targets)
        {
            return this.GetDamage(this.Duration, targets);
        }

        /// <summary>
        ///     Releases the unstable concoction.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public override bool UseAbility(Unit target)
        {
            return this.ThrowAbility.CanBeCasted && this.ThrowAbility.UseAbility(target);
        }
    }
}