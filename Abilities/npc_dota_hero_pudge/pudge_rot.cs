// <copyright file="pudge_rot.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_pudge
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class pudge_rot : ToggleAbility, IAreaOfEffectAbility, IHasDot, IHasHealthCost
    {
        public pudge_rot(Ability ability)
            : base(ability)
        {
        }

        public float Duration { get; } = 0.5f;

        public bool HasInitialDamage { get; } = false;

        /// <summary>
        ///     Gets the health cost for each tick of the dot.
        /// </summary>
        public float HealthCost
        {
            get
            {
                var amplify = this.Owner.GetSpellAmplification();
                var reduction = this.Ability.GetDamageReduction(this.Owner);
                return DamageHelpers.GetSpellDamage(this.RawTickDamage, amplify, reduction);
            }
        }

        public string ModifierName { get; } = "modifier_pudge_rot";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("rot_radius");
            }
        }

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("rot_damage");
            }
        }

        public string TargetModifierName { get; } = "modifier_pudge_rot";

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("rot_tick");
            }
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Owner.GetSpellAmplification();

            var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_pudge_2);
            if (talent != null && talent.Level > 0)
            {
                damage += talent.GetAbilitySpecialData("value");
            }

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        public float GetTotalDamage(params Unit[] target)
        {
            return this.GetTickDamage(target) * (this.Duration / this.TickRate);
        }
    }
}