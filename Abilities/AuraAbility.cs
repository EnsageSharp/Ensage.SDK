// <copyright file="AuraAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using Ensage.SDK.Extensions;

    public abstract class AuraAbility : PassiveAbility, IAuraAbility
    {
        protected AuraAbility(Ability ability)
            : base(ability)
        {
        }

        public virtual string AuraModifierName { get; } = string.Empty;

        public virtual float AuraRadius
        {
            get
            {
                return this.Ability.GetCastRange();
            }
        }
    }
}