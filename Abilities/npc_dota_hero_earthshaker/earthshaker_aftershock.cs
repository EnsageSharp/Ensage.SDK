// <copyright file="earthshaker_aftershock.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_earthshaker
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using SharpDX;

    public class earthshaker_aftershock : PassiveAbility, IAreaOfEffectAbility
    {
        public earthshaker_aftershock(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("aftershock_range");
            }
        }

        public float GetDamage(Vector3 myPosition, params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();

            foreach (var target in targets.Where(x => x.Distance2D(myPosition) <= this.Radius))
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        public override float GetDamage(params Unit[] targets)
        {
            return this.GetDamage(this.Owner.Position, targets);
        }
    }
}