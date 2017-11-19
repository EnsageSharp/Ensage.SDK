// <copyright file="chen_penitence.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_chen
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class chen_penitence : RangedAbility, IHasDamageAmplifier, IHasTargetModifier
    {
        public chen_penitence(Ability ability)
            : base(ability)
        {
        }

        public DamageType AmplifierType { get; } = DamageType.Magical | DamageType.Physical | DamageType.Pure;

        public float DamageAmplification
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("bonus_damage_taken") / 100f;
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_chen_penitence";
    }
}