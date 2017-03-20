namespace Ensage.SDK.Abilities.npc_dota_hero_enigma
{
    using System;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class enigma_black_hole : CircleAbility, IDotAbility
    {
        public enigma_black_hole(Ability ability)
            : base(ability)
        {
        }

        public float Duration => this.Ability.GetAbilitySpecialData("duration");

        public string ModifierName { get; } = "TODO";

        public override float Radius => this.Ability.GetAbilitySpecialData("pull_radius");

        public float TickRate => this.Ability.GetAbilitySpecialData("tick_rate");

        public override float GetDamage(params Unit[] target)
        {
            return this.GetTickDamage(target);
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var nearRadius = this.Ability.GetAbilitySpecialData("near_radius");
            var nearDamage = this.Ability.GetAbilitySpecialData("near_damage");

            var farRadius = this.Ability.GetAbilitySpecialData("far_radius");
            var farDamage = this.Ability.GetAbilitySpecialData("far_damage");

            var amplify = this.Ability.SpellAmplification();

            var owner = this.Ability.Owner;

            var totalDamage = 0.0f;
            foreach (var target in targets)
            {
                if (!this.CanAffectTarget(target))
                {
                    continue;
                }

                float damage;
                var distance = target.Distance2D(owner);
                if (distance <= nearRadius)
                {
                    damage = nearDamage;
                }
                else if (distance <= farRadius)
                {
                    damage = farDamage;
                }
                else
                {
                    continue;
                }

                var reduction = this.Ability.GetDamageReduction(target);
                totalDamage += DamageHelpers.GetSpellDamage(damage, amplify, reduction);
            }

            return totalDamage;
        }

        public float GetTotalDamage(params Unit[] target)
        {
            return this.GetTickDamage(target) * (this.Duration / this.TickRate);
        }
    }
}