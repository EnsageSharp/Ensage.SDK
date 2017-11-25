// <copyright file="item_bloodthorn.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_bloodthorn : RangedAbility, IHasTargetModifier, IHasDamageAmplifier
    {
        public item_bloodthorn(Item item)
            : base(item)
        {
        }

        public DamageType AmplifierType { get; } = DamageType.Physical | DamageType.Magical | DamageType.Pure;

        public override UnitState AppliesUnitState { get; } = UnitState.Silenced | UnitState.EvadeDisabled;

        public float DamageAmplification
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("silence_damage_percent") / 100f;
            }
        }

        public override DamageType DamageType
        {
            get
            {
                return DamageType.Magical;
            }
        }

        public string TargetModifierName { get; } = "modifier_bloodthorn_debuff";
    }
}