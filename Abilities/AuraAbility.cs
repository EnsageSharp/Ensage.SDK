// <copyright file="AuraAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using Ensage.SDK.Extensions;

    public abstract class AuraAbility : BaseAbility
    {
        protected AuraAbility(Ability ability)
            : base(ability)
        {
        }

        public override float Range => this.Ability.GetAbilitySpecialData("aura_radius");

        public override float GetDamage(params Unit[] target)
        {
            return 0.0f;
        }
    }
}