// <copyright file="doom_bringer_infernal_blade.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_doom_bringer
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class doom_bringer_infernal_blade : OrbAbility, IHasDot
    {
        public doom_bringer_infernal_blade(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("burn_duration");
            }
        }

        public bool HasInitialDamage { get; } = false;

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("burn_damage");
            }
        }

        public string TargetModifierName { get; } = "modifier_doom_bringer_infernal_blade_burn";

        public float TickRate { get; } = 1.0f;

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Ability.SpellAmplification();
            var reduction = 0.0f;
            if (targets.Any())
            {
                var percentageDamage = this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "burn_damage_pct") / 100f;
                damage += targets.First().MaximumHealth * percentageDamage;
                reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetDamage(targets) + (this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate));
        }
    }
}