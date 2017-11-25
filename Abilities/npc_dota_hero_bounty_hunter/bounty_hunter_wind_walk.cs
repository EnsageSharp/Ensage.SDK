// <copyright file="bounty_hunter_wind_walk.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bounty_hunter
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class bounty_hunter_wind_walk : ActiveAbility, IHasModifier
    {
        public bounty_hunter_wind_walk(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Invisible;

        public string ModifierName { get; } = "modifier_bounty_hunter_wind_walk";

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