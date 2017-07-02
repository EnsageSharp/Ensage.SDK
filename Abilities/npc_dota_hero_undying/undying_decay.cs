// <copyright file="undying_decay.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_undying
{
    using System;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using PlaySharp.Toolkit.Helper.Annotations;

    public class undying_decay : CircleAbility, IHasTargetModifier, IHasModifier
    {
        public undying_decay(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_undying_decay_buff_counter";

        public override float Speed { get; } = float.MaxValue;

        public int StrengthSteal
        {
            get
            {
                return (int)this.Ability.GetAbilitySpecialData(this.Owner.HasAghanimsScepter() ? "str_steal_scepter" : "str_steal");
            }
        }

        public string TargetModifierName { get; } = "modifier_undying_decay_debuff_counter";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("decay_damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = this.RawDamage;
            var amplify = this.Ability.SpellAmplification();

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        /// <summary>
        ///     Returns the health of the target with the strength removed and damage applied of decay.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public virtual float GetHealthLeft([NotNull] Unit target)
        {
            var health = (float)target.Health;

            var hero = target as Hero;
            if (hero != null)
            {
                var steal = this.StrengthSteal;
                var diff = (int)hero.TotalStrength - steal;
                if (diff < 0)
                {
                    steal = (int)hero.TotalStrength;
                }

                health -= steal * 20;
            }

            return Math.Max(0, health - this.GetDamage(target));
        }
    }
}