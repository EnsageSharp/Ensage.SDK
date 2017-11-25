// <copyright file="leshrac_diabolic_edict.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_leshrac
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class leshrac_diabolic_edict : ActiveAbility, IAreaOfEffectAbility, IHasModifier, IHasDot
    {
        public leshrac_diabolic_edict(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                return this.Duration;
            }
        }

        public bool HasInitialDamage { get; } = false;

        public string ModifierName { get; } = "modifier_leshrac_diabolic_edict";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public float RawTickDamage
        {
            get
            {
                return base.RawDamage;
            }
        }

        public string TargetModifierName { get; } = string.Empty;

        public float TickRate
        {
            get
            {
                var explosions = this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "num_explosions");
                return this.DamageDuration / explosions;
            }
        }

        protected override float RawDamage
        {
            get
            {
                return 0;
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