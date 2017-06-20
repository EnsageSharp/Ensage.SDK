// <copyright file="item_invis_sword.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_invis_sword : ActiveAbility, IHasModifier
    {
        public item_invis_sword(Item item)
            : base(item)
        {
        }

        public override DamageType DamageType
        {
            get
            {
                return DamageType.Physical;
            }
        }

        public string ModifierName { get; } = "modifier_item_invisibility_edge_windwalk";
    }
}