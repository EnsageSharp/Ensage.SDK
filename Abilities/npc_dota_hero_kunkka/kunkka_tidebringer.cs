// <copyright file="kunkka_tidebringer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_kunkka
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class kunkka_tidebringer : OrbAbility
    {
        public kunkka_tidebringer(Ability ability)
            : base(ability)
        {
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage_bonus");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return base.GetDamage() + this.RawDamage;
            }

            var reduction = this.Ability.GetDamageReduction(targets.First(), this.DamageType);
            var amplify = this.Ability.SpellAmplification();

            return base.GetDamage(targets) + DamageHelpers.GetSpellDamage(this.RawDamage, amplify, reduction);
        }
    }
}