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

        public float DamageDuration
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
        public string TargetModifierName { get; } = string.Empty;

        public float TickRate { get; } = 1.0f;

        public float GetTickDamage(params Unit[] targets)
        {
            var damagePercent = this.RawTickDamage / 100.0f;

            return targets.Select(unit => (float)unit.MaximumHealth).Select(maxHealth => maxHealth * damagePercent).Sum();
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate);
        }
    }
}