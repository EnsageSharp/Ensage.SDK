// <copyright file="item_spirit_vessel.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class item_spirit_vessel : RangedAbility, IHasDot, IHasHealthRestore
    {
        public item_spirit_vessel(Item item)
            : base(item)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return this.Item.CurrentCharges > 0 && base.CanBeCasted;
            }
        }

        public float DamageDuration
        {
            get
            {
                return this.Duration;
            }
        }

        public override DamageType DamageType { get; } = DamageType.Magical;

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public bool HasInitialDamage { get; } = false;

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("soul_damage_amount");
            }
        }

        public string TargetModifierName { get; } = "modifier_item_spirit_vessel_damage";

        public float TickRate { get; } = 1.0f;

        public float TotalHealthRestore
        {
            get
            {
                var regen = this.Ability.GetAbilitySpecialData("soul_heal_amount");
                var hpPercentage = this.Owner.Health * (this.Ability.GetAbilitySpecialData("ally_hp_gain") / 100f); // approx
                return (regen + hpPercentage) * this.Duration;
            }
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Ability.SpellAmplification();
            var drain = this.Ability.GetAbilitySpecialData("enemy_hp_drain");
            var reduction = 0.0f;
            if (targets.Any())
            {
                var drainAmount = (targets.First().Health * drain) / 100;
                damage += drainAmount;
                reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate);
        }
    }
}