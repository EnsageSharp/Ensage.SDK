// <copyright file="legion_commander_overwhelming_odds.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_legion_commander
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    using SharpDX;

    public class legion_commander_overwhelming_odds : CircleAbility, IHasModifier
    {
        public legion_commander_overwhelming_odds(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_legion_commander_overwhelming_odds";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }

        public float GetDamage(Vector3 castPosition, params Unit[] targets)
        {
            var damage = this.RawDamage;

            if (!targets.Any())
            {
                return damage;
            }

            var otherTargets = EntityManager<Unit>.Entities.Where(
                                                         x => x.IsValid
                                                           && x.IsAlive
                                                           && x.IsVisible
                                                           && x.IsSpawned
                                                           && !(x is Building)
                                                           && x.IsEnemy(this.Owner)
                                                           && !x.IsInvulnerable()
                                                           && x.Distance2D(castPosition) < this.Radius);
            foreach (var otherTarget in otherTargets)
            {
                var hero = (otherTarget as Hero);
                if (hero != null && !hero.IsIllusion)
                {
                    damage += this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "damage_per_hero");
                    continue;
                }

                damage += this.Ability.GetAbilitySpecialData("damage_per_unit");
            }

            var totalDamage = 0.0f;
            var amplify = this.Owner.GetSpellAmplification();

            foreach (var target in targets)
            {
                var reduction = this.Ability.GetDamageReduction(target, this.DamageType);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        public override float GetDamage(params Unit[] targets)
        {
            return this.GetDamage(targets.First().Position, targets);
        }
    }
}