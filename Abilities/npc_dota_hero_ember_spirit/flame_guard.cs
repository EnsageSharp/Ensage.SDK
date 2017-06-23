// <copyright file="ember_spirit_flame_guard.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_ember_spirit
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class ember_spirit_flame_guard : ActiveAbility, IAreaOfEffectAbility, IHasDot, IHasModifier
    {
        public ember_spirit_flame_guard(Ability ability)
            : base(ability)
        {
        }

        public float Duration
        {
            get
            {
                var level = this.Ability.Level;
                if (level == 0)
                {
                    return 0.0f;
                }

                return this.Ability.GetDuration(level - 1);
            }
        }

        public bool HasInitialDamage { get; } = false;

        public string ModifierName { get; } = "modifier_ember_spirit_flame_guard";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("flame_guard_radius");
            }
        }

        public float RawTickDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("flame_guard_damage");

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
                return this.Ability.GetAbilitySpecialData("flame_guard_damage_tick");
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