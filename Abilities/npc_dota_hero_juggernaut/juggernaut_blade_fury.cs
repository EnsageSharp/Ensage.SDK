// <copyright file="juggernaut_blade_fury.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_juggernaut
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class juggernaut_blade_fury : ActiveAbility, IAreaOfEffectAbility, IHasDot, IHasModifier
    {
        public juggernaut_blade_fury(Ability ability)
            : base(ability)
        {
        }

        public float Duration
        {
            get
            {
                var duration = this.Ability.GetAbilitySpecialData("duration");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_juggernaut);
                if (talent?.Level > 0)
                {
                    duration += talent.GetAbilitySpecialData("value");
                }

                return duration;
            }
        }

        public bool HasInitialDamage { get; } = false;

        public string ModifierName { get; } = "modifier_juggernaut_blade_fury";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("blade_fury_radius");
            }
        }

        public float RawTickDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("blade_fury_damage");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_juggernaut_3);
                if (talent != null && talent.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage * this.TickRate;
            }
        }

        public string TargetModifierName { get; } = string.Empty;

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("blade_fury_damage_tick");
            }
        }

        public override bool CanHit(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return true;
            }

            return targets.All(x => x.Distance2D(this.Owner) < this.Radius);
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Ability.SpellAmplification();

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets) * (this.Duration / this.TickRate);
        }
    }
}