// <copyright file="item_orb_of_venom.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_orb_of_venom : PassiveAbility //, IHasDot
    {
        public item_orb_of_venom(Item item)
            : base(item)
        {
        }

        public override DamageType DamageType
        {
            get
            {
                return DamageType.Magical;
            }
        }
    }
}