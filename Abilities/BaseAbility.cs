namespace Ensage.SDK.Abilities
{
    public abstract class BaseAbility : IAbility
    {
        protected BaseAbility(Ability ability)
        {
            Ability = ability;
        }

        public Ability Ability { get; }
        public abstract float GetDamage(params Unit[] target);
    }
}
