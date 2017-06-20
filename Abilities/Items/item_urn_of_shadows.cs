// <copyright file="item_urn_of_shadows.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Extensions;

    public class item_urn_of_shadows : RangedAbility, IHasDot
    {
        public item_urn_of_shadows(Item item)
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
                return DamageType.Pure;
            }
        }

        public float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("soul_damage_duration");
            }
        }

        public bool HasInitialDamage { get; } = false;

        public float RawTickDamage
        {
            get
            {
                return this.GetTotalDamage() / this.Duration;
            }
        }

        // modifier_item_urn_heal
        public string TargetModifierName { get; } = "modifier_item_urn_damage";

        public float TickRate { get; } = 1.0f;

        public float GetTickDamage(params Unit[] targets)
        {
            return this.RawTickDamage;
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.Ability.GetAbilitySpecialData("soul_damage_amount");
        }
    }
}