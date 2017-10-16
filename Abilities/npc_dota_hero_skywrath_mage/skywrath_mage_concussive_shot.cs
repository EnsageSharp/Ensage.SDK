// <copyright file="skywrath_mage_concussive_shot.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_skywrath_mage
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using PlaySharp.Toolkit.Helper.Annotations;

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
                return this.Ability.GetAbilitySpecialData("launch_radius") - 25f;  // bug fixed
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("speed");
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

        public Hero TargetHit
        {
            get
            {
                return EntityManager<Hero>.Entities.OrderBy(x => 
                              x.Distance2D(this.Owner)).FirstOrDefault(x =>
                                    x.IsAlive &&
                                    x.IsVisible &&
                                    !x.IsIllusion &&
                                    x.IsValid &&
                                    x.IsEnemy(Owner) &&
                                    x.Distance2D(Owner) <= this.Radius);
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            if (!targets.Any(x => x == TargetHit))
            {
                return 0;
            }

            var totalDamage = 0.0f;

            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();
            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        public override float GetDamage([NotNull] Unit target, float damageModifier, float targetHealth = float.MinValue)
        {
            if (TargetHit != target)
            {
                return 0;
            }

            var amplify = this.Owner.GetSpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target, this.DamageType);

            return DamageHelpers.GetSpellDamage(this.RawDamage, amplify, -reduction, damageModifier);
        }

        public override bool CanHit(params Unit[] targets)
        {
            return targets.All(x => x.Distance2D(this.Owner) < this.Radius);
        }
    }
}