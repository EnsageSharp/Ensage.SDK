// <copyright file="nyx_assassin_vendetta.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_nyx_assassin
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
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
            if (!targets.Any())
            {
                return 0;
            }

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            var reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);

            return this.Owner.GetAttackDamage(targets.First()) + DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }
    }
}