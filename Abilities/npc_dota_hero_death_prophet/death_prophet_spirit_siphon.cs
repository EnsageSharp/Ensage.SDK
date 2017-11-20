// <copyright file="death_prophet_spirit_siphon.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_death_prophet
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class death_prophet_spirit_siphon : RangedAbility, IHasDot, IHasModifier
    {
        public death_prophet_spirit_siphon(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("haunt_duration");
            }
        }

        public bool HasInitialDamage { get; } = false;

        public string ModifierName { get; } = "modifier_death_prophet_spirit_siphon";

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage") * this.TickRate;
            }
        }

        public string TargetModifierName { get; } = "modifier_death_prophet_spirit_siphon_slow";

        public float TickRate { get; } = 0.25f;

        public float GetTickDamage(params Unit[] targets)
        {
            var target = targets.FirstOrDefault();
            if (target == null)
            {
                return 0;
            }

            var damagePct = this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "damage_pct") / 100f;
            var damage = this.RawTickDamage + (damagePct * target.MaximumHealth * this.TickRate);
            var amplify = this.Ability.SpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target, this.DamageType);

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate);
        }
    }
}