// <copyright file="earthshaker_echo_slam.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_earthshaker
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using SharpDX;

    public class earthshaker_echo_slam : ActiveAbility, IAreaOfEffectAbility, IHasTargetModifierTexture
    {
        private readonly earthshaker_aftershock aftershock;

        public earthshaker_echo_slam(Ability ability)
            : base(ability)
        {
            var passive = this.Owner.GetAbilityById(AbilityId.earthshaker_aftershock);
            if (passive != null)
            {
                this.aftershock = new earthshaker_aftershock(passive);
            }
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("echo_slam_damage_range");
            }
        }

        public string[] TargetModifierTextureName { get; } = { "earthshaker_aftershock" };

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "echo_slam_echo_damage");
            }
        }

        public float GetDamage(Vector3 myPosition, params Unit[] targets)
        {
            if (!targets.Any())
            {
                return 0;
            }

            var otherTargets = EntityManager<Unit>.Entities.Where(
                                                      x => x.IsValid
                                                           && x.IsAlive
                                                           && x.IsEnemy(this.Owner)
                                                           && x.Distance2D(myPosition) < this.Ability.GetAbilitySpecialData("echo_slam_echo_search_range"))
                                                  .Except(targets);

            var multiplier = 0;
            foreach (var otherTarget in otherTargets)
            {
                if (otherTarget is Hero)
                {
                    multiplier++;
                }

                multiplier++;
            }

            var totalDamage = 0.0f;
            var damage = this.RawDamage;
            var amplify = this.Owner.GetSpellAmplification();

            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage * multiplier, amplify, reduction);
            }

            return totalDamage + (this.aftershock?.GetDamage(myPosition, targets) ?? 0);
        }

        public override float GetDamage(params Unit[] targets)
        {
            return this.GetDamage(this.Owner.Position, targets);
        }
    }
}