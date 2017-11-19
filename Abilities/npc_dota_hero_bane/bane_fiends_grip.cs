// <copyright file="bane_fiends_grip.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bane
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class bane_fiends_grip : RangedAbility, IHasDot
    {
        public bane_fiends_grip(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "fiend_grip_duration");
            }
        }

        public bool HasInitialDamage { get; } = true;

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("fiend_grip_damage") * this.TickRate;
            }
        }

        public string TargetModifierName { get; } = "modifier_bane_fiends_grip";

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("fiend_grip_tick_interval");
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