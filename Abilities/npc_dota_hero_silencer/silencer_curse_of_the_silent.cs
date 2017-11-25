// <copyright file="silencer_curse_of_the_silent.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_silencer
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class silencer_curse_of_the_silent : CircleAbility, IHasDot
    {
        public silencer_curse_of_the_silent(Ability ability)
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
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "damage");
            }
        }

        public string TargetModifierName { get; } = "modifier_silencer_curse_of_the_silent";

        public float TickRate { get; } = 1.0f;

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = this.RawTickDamage;
            var amplify = this.Owner.GetSpellAmplification();

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target);
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