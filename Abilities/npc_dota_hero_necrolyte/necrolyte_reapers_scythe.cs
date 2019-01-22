// <copyright file="slardar_amplify_damage.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

using System;
using Ensage;
using Ensage.SDK.Abilities;
using Ensage.SDK.Abilities.Components;
using Ensage.SDK.Extensions;
using Ensage.SDK.Helpers;
using PlaySharp.Toolkit.Helper.Annotations;

namespace wtf.Sdk.Abilities.npc_dota_hero_necrolyte
{
    public class necrolyte_reapers_scythe : RangedAbility, IHasTargetModifier
    {
        public necrolyte_reapers_scythe(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_necrolyte_reapers_scythe";

        public float DamagePerHealth
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_per_health",this.Ability.Level);
            }
        }

        public override float GetDamage([NotNull] Unit target, float damageModifier, float targetHealth = float.MinValue)
        {
            var damage = (target.MaximumHealth-targetHealth)* DamagePerHealth;
            if (damage <= 0)
            {
                return 0;
            }

            var amplify = this.Owner.GetSpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target, this.DamageType);

            return DamageHelpers.GetSpellDamage(damage, amplify, -reduction, damageModifier);
        }
    }
}