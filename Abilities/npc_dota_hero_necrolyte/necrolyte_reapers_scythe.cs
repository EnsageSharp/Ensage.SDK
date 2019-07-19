// <copyright file="slardar_amplify_damage.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

using System;
using System.Linq;
using Ensage;
using Ensage.SDK.Abilities;
using Ensage.SDK.Abilities.Components;
using Ensage.SDK.Extensions;
using Ensage.SDK.Helpers;
using PlaySharp.Toolkit.Helper.Annotations;

namespace Ensage.SDK.Abilities.npc_dota_hero_necrolyte
{
    public class necrolyte_reapers_scythe : RangedAbility, IHasTargetModifier
    {
        public necrolyte_reapers_scythe(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_necrolyte_reapers_scythe";

        public override DamageType DamageType { get; } = DamageType.Magical;

        public float DamagePerMissingHealth
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_per_health",this.Ability.Level);
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var target = targets.FirstOrDefault();
            if (target == null)
            {
                return this.RawDamage;
            }

            var missingHealthToDamage = (target.MaximumHealth - target.Health) * DamagePerMissingHealth;
            var amplify = this.Owner.GetSpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
            var damage = DamageHelpers.GetSpellDamage(this.RawDamage * missingHealthToDamage, amplify, reduction);

            return damage;
        }
    }
}