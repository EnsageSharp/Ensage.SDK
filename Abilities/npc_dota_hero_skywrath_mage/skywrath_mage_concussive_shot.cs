// <copyright file="skywrath_mage_concussive_shot.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_skywrath_mage
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class skywrath_mage_concussive_shot : ActiveAbility, IAreaOfEffectAbility, IHasTargetModifier
    {
        public skywrath_mage_concussive_shot(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                if (this.Owner.GetAbilityById(AbilityId.special_bonus_unique_skywrath_4)?.Level > 0)
                {
                    return float.MaxValue;
                }

                return this.Ability.GetAbilitySpecialData("launch_radius");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("speed");
            }
        }

        public Hero TargetHit
        {
            get
            {
                return EntityManager<Hero>.Entities.Where(x => x.IsValid && x.IsVisible && x.IsAlive && !x.IsIllusion && x.IsEnemy(this.Owner) && this.CanHit(x))
                                          .OrderBy(x => x.Distance2D(this.Owner))
                                          .FirstOrDefault();
            }
        }

        public string TargetModifierName { get; } = "modifier_skywrath_mage_concussive_shot_slow";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }

        public override bool CanHit(params Unit[] targets)
        {
            return targets.All(x => x.Distance2D(this.Owner) < (this.Radius - this.Owner.HullRadius));
        }

        public override float GetDamage(params Unit[] targets)
        {
            var targetHit = this.TargetHit;
            if (!targets.Any(x => x == targetHit))
            {
                return 0;
            }

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            var reduction = this.Ability.GetDamageReduction(targetHit, this.DamageType);

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public override float GetDamage(Unit target, float damageModifier, float targetHealth = float.MinValue)
        {
            var targetHit = this.TargetHit;
            if (targetHit != target)
            {
                return 0;
            }

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            var reduction = this.Ability.GetDamageReduction(targetHit, this.DamageType);

            return DamageHelpers.GetSpellDamage(damage, amplify, -reduction, damageModifier);
        }
    }
}