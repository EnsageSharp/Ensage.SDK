// <copyright file="enigma_malefice.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_enigma
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class enigma_malefice : TargetAbility, IDotAbility
    {
        public enigma_malefice(Ability ability)
            : base(ability)
        {
        }

        public float Duration => this.Ability.GetAbilitySpecialData("duration");

        public string ModifierName { get; } = "TODO";

        public float TickDamage => this.Ability.GetAbilitySpecialData("damage");

        public float TickRate => this.Ability.GetAbilitySpecialData("tick_rate");

        public override float GetDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets);
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var target = targets.First();
            if (!this.CanAffectTarget(target))
            {
                return 0;
            }

            var damage = this.TickDamage;
            var amplify = this.Ability.SpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target);

            return damage * (1.0f + amplify) * (1.0f - reduction);
        }

        public float GetTotalDamage(params Unit[] target)
        {
            return this.GetTickDamage(target) * (this.Duration / this.TickRate);
        }
    }
}