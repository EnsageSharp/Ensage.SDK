// <copyright file="PassiveAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    public abstract class PassiveAbility : BaseAbility
    {
        protected PassiveAbility(Ability ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return this.IsReady && !this.Owner.UnitState.HasFlag(UnitState.PassivesDisabled);
            }
        }
    }
}