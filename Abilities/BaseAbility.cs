// <copyright file="BaseAbility.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities
{
    using PlaySharp.Toolkit.Helper.Annotations;

    public abstract class BaseAbility
    {
        protected BaseAbility(Ability ability)
        {
            this.Ability = ability;
        }

        public Ability Ability { get; }

        public virtual float ActivationDelay { get; } = 0;

        public virtual UnitState AppliesUnitState { get; } = 0;

        public abstract bool CanBeCasted { get; }

        public virtual float CastRange
        {
            get
            {
                return this.BaseCastRange;
            }
        }

        public virtual DamageType DamageType
        {
            get
            {
                return this.Ability.DamageType;
            }
        }

        public virtual float Duration
        {
            get
            {
                var level = this.Ability.Level;
                if (level == 0)
                {
                    return 0.0f;
                }

                return this.Ability.GetDuration(level - 1);
            }
        }

        public virtual bool IsReady
        {
            get
            {
                if (this.Ability.Level == 0 || this.Ability.Cooldown > 0)
                {
                    return false;
                }

                if (this.Owner.Mana < this.ManaCost)
                {
                    return false;
                }

                return true;
            }
        }

        [CanBeNull]
        public Item Item
        {
            get
            {
                return this.Ability as Item;
            }
        }

        public float ManaCost
        {
            get
            {
                return this.Ability.ManaCost;
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

        protected virtual float BaseCastRange { get; } = 0;

        protected virtual float RawDamage
        {
            get
            {
                var level = this.Ability.Level;
                if (level == 0)
                {
                    return 0;
                }

                return this.Ability.GetDamage(level - 1);
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

        public virtual float GetDamage(Unit target, float damageModifier, float targetHealth = float.MinValue)
        {
            return 0;
        }

        public override string ToString()
        {
            return this.Ability.Name;
        }
    }
}