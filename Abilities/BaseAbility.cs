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

        public abstract bool CanBeCasted { get; }

        public virtual bool IsReady
        {
            get
            {
                if (this.Ability.Level == 0 || this.Ability.IsHidden || this.Ability.Cooldown > 0)
                {
                    return false;
                }

                if (this.Owner.Mana < this.Ability.ManaCost)
                {
                    return false;
                }

                return true;
            }
        }

        public virtual float CastRange { get; } = 0;

        public virtual DamageType DamageType
        {
            get
            {
                return this.Ability.DamageType;
            }
        }

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

        protected virtual float RawDamage
        {
            get
            {
                return 0;
            }
        }

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
    }
}