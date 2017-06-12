// <copyright file="huskar_burning_spear.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_huskar
{

    using System.Linq;

    using Ensage.SDK.Extensions;

    using Ensage.SDK.Helpers;

    using Ensage.Common.Extensions;


    public class huskar_burning_spear : OrbAbility, IHasLifeCost, IHasDot
    {
        public huskar_burning_spear(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName => "modifier_huskar_burning_spear_counter";

        public float HealthCost => Ability.GetAbilitySpecialData("health_cost");

        public bool HasInitialDamage => false;

        public float Duration => 8.0f;

        public float TickRate => 1.0f;

        public float TickDamage => GetRawDamage();

        public float GetTickDamage(params Unit[] targets)
        {
            var damage = 0.0f;

            var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_huskar_2);

            var amplify = 1.0f + Owner.GetSpellAmplification();

            var target = targets.First();

            var reduction = this.Ability.GetDamageReduction(target);

            if (talent != null && talent.Level > 0)
            {
                var talentDamage = talent.GetAbilitySpecialData("value");
                damage += talentDamage + TickDamage;
            }
            else
            {
                damage += TickDamage;
            }

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public override float GetDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets);
        }

        public float GetTotalDamage(params Unit[] target)
        {
            var damage = base.GetDamage(target);
            return (this.GetTickDamage(target) * (this.Duration / this.TickRate)) + base.GetDamage(target);
        }
    }
}

