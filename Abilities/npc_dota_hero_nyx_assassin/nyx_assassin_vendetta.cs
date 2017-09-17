// <copyright file="nyx_assassin_vendetta.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_nyx_assassin
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class nyx_assassin_vendetta : ActiveAbility, IHasModifier
    {
        public nyx_assassin_vendetta(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_nyx_assassin_vendetta";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("bonus_damage");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                var reduction = target.DamageResist;
                var attackDamage = this.Owner.GetAttackDamage(target);
                totalDamage = attackDamage + DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}