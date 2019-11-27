// <copyright file="item_veil_of_discord.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.Items
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    using PlaySharp.Toolkit.Helper.Annotations;

    [PublicAPI]
    public class item_veil_of_discord : CircleAbility, IHasTargetModifier, IHasDamageAmplifier
    {
        public item_veil_of_discord(Item item)
            : base(item)
        {
        }

        public DamageType AmplifierType { get; } = DamageType.All;

        public float DamageAmplification
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("spell_amp") / -100.0f; // -20 to all abilities
            }
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("debuff_radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_item_veil_of_discord_debuff";
    }
}