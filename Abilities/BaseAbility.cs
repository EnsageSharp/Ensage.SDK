// <copyright file="BaseAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    public abstract class BaseAbility
    {
        protected BaseAbility(Ability ability)
        {
            this.Ability = ability;
        }

        public Ability Ability { get; }

        public Item Item
        {
            get
            {
                return this.Ability as Item;
            }
        }

        public Unit Owner
        {
            get
            {
                return (Unit)this.Ability.Owner;
            }
        }

        public virtual SpellPierceImmunityType PiercesSpellImmunity
        {
            get
            {
                return this.Ability.SpellPierceImmunityType;
            }
        }

        public virtual float Range { get; } = 0;

        public static implicit operator Item(BaseAbility ability)
        {
            return ability.Item;
        }

        public static implicit operator Ability(BaseAbility ability)
        {
            return ability.Ability;
        }

        public virtual float GetDamage(params Unit[] targets)
        {
            return 0;
        }

        public override string ToString()
        {
            return this.Ability.Name;
        }

        protected virtual float GetRawDamage()
        {
            return 0;
        }
    }
}