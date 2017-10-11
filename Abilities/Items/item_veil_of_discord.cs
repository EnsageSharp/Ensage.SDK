// <copyright file="item_veil_of_discord.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using PlaySharp.Toolkit.Helper.Annotations;

    [PublicAPI]
    public class item_veil_of_discord : AreaOfEffectAbility, IHasTargetModifier, IHasDamageAmplifier
    {
        public item_veil_of_discord(Item item)
            : base(item)
        {
        }

        public string TargetModifierName { get; } = "modifier_item_veil_of_discord_debuff";

        public DamageType AmplifierType { get; } = DamageType.Magical;

        public float DamageAmplification
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("resist_debuff") / -100.0f; // -25
            }
        }

        protected override string RadiusName { get; } = "debuff_radius";
    }
}