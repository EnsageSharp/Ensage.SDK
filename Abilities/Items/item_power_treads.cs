// <copyright file="item_power_treads.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.Items;

    public class item_power_treads : ActiveAbility
    {
        public item_power_treads(Item item)
            : base(item)
        {
        }

        public Attribute ActiveAttribute
        {
            get
            {
                return ((PowerTreads)this.Item).ActiveAttribute;
            }
        }

        public bool SwitchAttribute(Attribute attribute)
        {
            if (!this.CanBeCasted)
            {
                return false;
            }

            var result = false;
            var activeAttribute = this.ActiveAttribute;

            switch (attribute)
            {
                case Attribute.Strength:
                    if (activeAttribute == Attribute.Intelligence)
                    {
                        result = this.Ability.UseAbility() && this.Ability.UseAbility();
                    }
                    else if (activeAttribute == Attribute.Agility)
                    {
                        result = this.Ability.UseAbility();
                    }
                    break;
                case Attribute.Intelligence:
                    if (activeAttribute == Attribute.Agility)
                    {
                        result = this.Ability.UseAbility() && this.Ability.UseAbility();
                    }
                    else if (activeAttribute == Attribute.Strength)
                    {
                        result = this.Ability.UseAbility();
                    }
                    break;
                case Attribute.Agility:
                    if (activeAttribute == Attribute.Strength)
                    {
                        result = this.Ability.UseAbility() && this.Ability.UseAbility();
                    }
                    else if (activeAttribute == Attribute.Intelligence)
                    {
                        result = this.Ability.UseAbility();
                    }
                    break;
            }

            if (result)
            {
                this.LastCastAttempt = Game.RawGameTime;
            }

            return result;
        }
    }
}