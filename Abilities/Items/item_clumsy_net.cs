// <copyright file="item_clumsy_net.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_clumsy_net : RangedAbility, IHasTargetModifier, IHasModifier
    {
        public item_clumsy_net(Item item)
            : base(item)
        {
        }


        public string TargetModifierName { get; } = "modifier_clumsy_net_ensnare";
        public string ModifierName { get; } = "modifier_clumsy_net_ensnare";
    }
}