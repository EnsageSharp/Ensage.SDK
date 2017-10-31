// <copyright file="item_spirit_vessel.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using System.Linq;
using Ensage.SDK.Helpers;

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_spirit_vessel : RangedAbility, IHasDot
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

        public override DamageType DamageType
        {
            get
            {
                return DamageType.Magical;
            }
        }

        public float Duration
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

        public string TargetModifierName { get; } = "modifier_item_spirit_vessel_damage"; //todo: get correct name

        public float TickRate { get; } = 1.0f;

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Ability.SpellAmplification();
            var drain = Ability.GetAbilitySpecialData("enemy_hp_drain");
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
            if (targets.Any())
            {
                return this.GetTickDamage(targets) * this.TickRate;
            }

            return this.GetTickDamage(targets) * (this.Duration / this.TickRate);
        }
    }
} 