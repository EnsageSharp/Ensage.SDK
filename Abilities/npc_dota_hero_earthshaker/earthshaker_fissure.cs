// <copyright file="earthshaker_fissure.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_earthshaker
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using SharpDX;

    public class earthshaker_fissure : LineAbility, IHasTargetModifier
    {
        private readonly earthshaker_aftershock aftershock;

        public earthshaker_fissure(Ability ability)
            : base(ability)
        {
            var passive = this.Owner.GetAbilityById(AbilityId.earthshaker_aftershock);
            if (passive != null)
            {
                this.aftershock = new earthshaker_aftershock(passive);
            }
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("fissure_radius");
            }
        }

        public override float Range
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "fissure_range");
            }
        }

        public override float Speed { get; } = float.MaxValue;

        public string TargetModifierName { get; } = "modifier_earthshaker_fissure_stun";

        public float GetDamage(Vector3 myPosition, params Unit[] targets)
        {
            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage + (this.aftershock?.GetDamage(myPosition, targets) ?? 0);
        }

        public override float GetDamage(params Unit[] targets)
        {
            return this.GetDamage(this.Owner.Position, targets);
        }
    }
}