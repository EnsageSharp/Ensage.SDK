// <copyright file="item_glimmer_cape.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;

    public class item_glimmer_cape : RangedAbility, IHasTargetModifier, IHasModifierTexture
    {
        public item_glimmer_cape(Item item)
            : base(item)
        {
        }

        public string[] ModifierTextureName { get; } = { "item_glimmer_cape" };

        // only lasts while target is fading (0.6sec) then modifier_invisible
        public string TargetModifierName { get; } = "modifier_item_glimmer_cape_fade";
    }
}