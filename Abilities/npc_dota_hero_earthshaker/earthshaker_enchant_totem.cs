// <copyright file="earthshaker_enchant_totem.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_earthshaker
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class earthshaker_enchant_totem : ActiveAbility, IHasModifier, IAreaOfEffectAbility
    {
        private readonly earthshaker_aftershock aftershock;

        public earthshaker_enchant_totem(Ability ability)
            : base(ability)
        {
            var passive = this.Owner.GetAbilityById(AbilityId.earthshaker_aftershock);
            if (passive != null)
            {
                this.aftershock = new earthshaker_aftershock(passive);
            }
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public float DamageMultiplier
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("totem_damage_percentage") / 100f;
            }
        }

        public string ModifierName { get; } = "modifier_earthshaker_enchant_totem";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("aftershock_range");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            return (this.aftershock?.GetDamage(targets) ?? 0);
        }
    }
}