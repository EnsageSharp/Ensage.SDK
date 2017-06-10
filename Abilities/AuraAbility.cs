namespace Ensage.SDK.Abilities
{
    using Ensage.SDK.Extensions;

    public abstract class AuraAbility : PassiveAbility
    {
        protected AuraAbility(Ability ability)
            : base(ability)
        {
        }

        public virtual float Radius
        {
            get
            {
                return this.Ability.GetCastRange();
            }
        }
    }
}