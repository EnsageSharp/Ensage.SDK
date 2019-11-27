// <copyright file="drow_ranger_multishot.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>
namespace Ensage.SDK.Abilities.npc_dota_hero_drow_ranger
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class drow_ranger_multishot : ConeAbility
    {
        public drow_ranger_multishot(Ability ability)
            : base(ability)
        {
        }

        public override float CastRange
        {
            get
            {
                return Owner.AttackRange * Ability.GetAbilitySpecialData("arrow_range_multiplier");
            }
        }

        public override DamageType DamageType { get; } = DamageType.Physical;

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                var damage = (Ability.GetAbilitySpecialData("arrow_damage_pct") / 100f) * Owner.GetAttackDamage(target, false, amplify); 
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}