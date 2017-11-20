// <copyright file="elder_titan_natural_order.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_elder_titan
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class elder_titan_natural_order : PassiveAbility, IHasDamageAmplifier
    {
        public elder_titan_natural_order(Ability ability)
            : base(ability)
        {
        }

        public DamageType AmplifierType { get; } = DamageType.Magical;

        public float DamageAmplification
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("magic_resistance_pct") / 100f;
            }
        }
    }
}