// <copyright file="item_rod_of_atos.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_rod_of_atos : RangedAbility, IHasTargetModifier
    {
        public item_rod_of_atos(Item item)
            : base(item)
        {
        }

        public string TargetModifierName { get; } = "modifier_rod_of_atos_debuff";
    }
}