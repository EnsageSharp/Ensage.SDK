// <copyright file="item_spell_prism.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class item_spell_prism : ActiveAbility, IHasModifier
    {
        public item_spell_prism(Item item)
            : base(item)
        {
        }

        public string ModifierName { get; } = "modifier_spell_prism_active";
    }
}