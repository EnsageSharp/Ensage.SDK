// <copyright file="item_shivas_guard.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class item_shivas_guard : ActiveAbility, IAreaOfEffectAbility, IAuraAbility, IHasTargetModifier
    {
        public item_shivas_guard(Item item)
            : base(item)
        {
        }

        public string AuraModifierName { get; } = "modifier_item_shivas_guard_aura";

        public float AuraRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("aura_radius");
            }
        }

        public override DamageType DamageType
        {
            get
            {
                return DamageType.Magical;
            }
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("blast_radius");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("blast_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_item_shivas_guard_blast";

        public override bool CanHit(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return true;
            }

            return this.Owner.Distance2D(targets.First()) < this.Radius;
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("blast_damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}
