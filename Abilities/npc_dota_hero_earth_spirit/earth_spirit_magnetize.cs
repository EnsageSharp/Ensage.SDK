// <copyright file="earth_spirit_magnetize.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_earth_spirit
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class earth_spirit_magnetize : ActiveAbility, IAreaOfEffectAbility, IHasDot
    {
        public earth_spirit_magnetize(Ability ability)
            : base(ability)
        {
        }

        public float DamageDuration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_duration");
            }
        }

        public bool HasInitialDamage { get; } = false;

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("cast_radius");
            }
        }

        public float RawTickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_per_second") * this.TickRate;
            }
        }

        public string TargetModifierName { get; } = "modifier_earth_spirit_magnetize";

        public float TickRate { get; } = 0.5f;

        public float GetTickDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.RawTickDamage;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        public float GetTotalDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets) * (this.DamageDuration / this.TickRate);
        }
    }
}