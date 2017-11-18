// <copyright file="ancient_apparition_ice_blast.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_ancient_apparition
{
    using System;
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using SharpDX;

    public class ancient_apparition_ice_blast : CircleAbility, IHasDot
    {
        public ancient_apparition_ice_blast(Ability ability)
            : base(ability)
        {
            var release = this.Owner.GetAbilityById(AbilityId.ancient_apparition_ice_blast_release);
            this.ReleaseAbility = new ancient_apparition_ice_blast_release(release);
        }

        public float DamageDuration
        {
            get
            {
                if (this.Owner.HasAghanimsScepter())
                {
                    return this.Ability.GetAbilitySpecialData("frostbite_duration_scepter");
                }

                return this.Ability.GetAbilitySpecialData("frostbite_duration");
            }
        }

        public bool HasInitialDamage { get; } = true;

        public float KillThreshold
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "kill_pct") / 100f;
            }
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("path_radius");
            }
        }

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("dot_damage");
            }
        }

        public ancient_apparition_ice_blast_release ReleaseAbility { get; }

        public string TargetModifierName { get; } = "modifier_ice_blast";

        public float TickRate { get; } = 1f;

        public float FinalRadius(Vector3 endPosition)
        {
            var minRadius = this.Ability.GetAbilitySpecialData("radius_min");
            var maxRadius = this.Ability.GetAbilitySpecialData("radius_max");
            var growRadius = this.Ability.GetAbilitySpecialData("radius_grow");
            var distance = this.Owner.Distance2D(endPosition);
            var duration = distance / this.Speed;

            return Math.Min(minRadius + (duration * growRadius), maxRadius);
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Ability.SpellAmplification();
            var reduction = 0.0f;
            if (targets.Any())
            {
                reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetDamage(targets) + (this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate));
        }

        public override bool UseAbility()
        {
            return this.ReleaseAbility.CanBeCasted && this.ReleaseAbility.UseAbility();
        }
    }
}