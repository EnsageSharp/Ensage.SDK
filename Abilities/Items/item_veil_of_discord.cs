// <copyright file="item_veil_of_discord.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    public class item_veil_of_discord : AreaOfEffectAbility, IHasTargetModifier
    {
        public item_veil_of_discord(Item item)
            : base(item)
        {
        }

        public string TargetModifierName { get; } = "modifier_item_veil_of_discord_debuff";

        protected override string RadiusName { get; } = "debuff_radius";
    }
}