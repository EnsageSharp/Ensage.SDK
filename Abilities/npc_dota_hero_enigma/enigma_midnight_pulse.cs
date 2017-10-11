// <copyright file="enigma_midnight_pulse.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_enigma
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class enigma_midnight_pulse : CircleAbility, IHasDot
    {
        public enigma_midnight_pulse(Ability ability)
            : base(ability)
        {
        }

        public float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public bool HasInitialDamage { get; } = false;

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_percent");
            }
        }

        // no modifier
        public string TargetModifierName { get; }

        public float TickRate { get; } = 1.0f;

        public float GetTickDamage(params Unit[] target)
        {
            var damagePercent = this.RawTickDamage / 100.0f;

            return target.Select(unit => (float)unit.MaximumHealth).Select(maxHealth => maxHealth * damagePercent).Sum();
        }

        public float GetTotalDamage(params Unit[] target)
        {
            return this.GetTickDamage(target) * (this.Duration / this.TickRate);
        }
    }
}