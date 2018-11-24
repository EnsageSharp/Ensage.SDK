// <copyright file="bounty_hunter_jinada.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bounty_hunter
{
    using System.Linq;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class bounty_hunter_jinada : PassiveAbility
    {
        public bounty_hunter_jinada(Ability ability)
            : base(ability)
        {
        }

        public override DamageType DamageType { get; } = DamageType.Physical;


        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("bonus_damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var target = targets.FirstOrDefault();
            var owner = this.Owner;
            if (target == null)
            {
                return this.RawDamage + owner.MinimumDamage + owner.BonusDamage;
            }

            var attackDamage = owner.GetAttackDamage(target);

            var amplify = this.Owner.GetSpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
            var abilityDamage = DamageHelpers.GetSpellDamage(this.RawDamage, amplify, reduction);

            return attackDamage + abilityDamage;
        }
    }
}