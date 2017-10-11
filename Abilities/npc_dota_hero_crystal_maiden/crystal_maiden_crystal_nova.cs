// <copyright file="crystal_maiden_crystal_nova.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_crystal_maiden
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class crystal_maiden_crystal_nova : CircleAbility, IAreaOfEffectAbility, IHasTargetModifier
    {
        public crystal_maiden_crystal_nova(Ability ability)
            : base(ability)
        {
        }

        public override float Speed
        {
            get
            {
                return float.MaxValue;
            }
        }

        public string TargetModifierName { get; } = "modifier_crystal_maiden_crystal_nova";

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("nova_damage");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_crystal_maiden_2);
                if (talent != null && talent.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }

        public override bool CanHit(params Unit[] targets)
        {
            return targets.All(x => x.Distance2D(this.Owner) <= (this.CastRange + this.Radius));
        }

        public override float GetDamage(params Unit[] targets)
        {
            var totalDamage = 0.0f;

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }
    }
}