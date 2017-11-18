// <copyright file="arc_warden_flux.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_arc_warden
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class arc_warden_flux : RangedAbility, IHasDot
    {
        public arc_warden_flux(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "duration");
            }
        }

        public bool HasInitialDamage { get; } = true;

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("search_radius");
            }
        }

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_per_second") * this.TickRate;
            }
        }

        public string TargetModifierName { get; } = "modifier_arc_warden_flux";

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("think_interval");
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.RawTickDamage;
            }
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
            return this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate);
        }
    }
}