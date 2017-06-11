// <copyright file="enigma_malefice.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_enigma
{
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    public class enigma_malefice : RangedAbility, IHasDot
    {
        public enigma_malefice(Ability ability)
            : base(ability)
        {
        }

        public float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public bool HasInitialDamage { get; } = false;

        public string TargetModifierName { get; } = "modifier_enigma_malefice";

        public float TickDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }

        public float TickRate
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("tick_rate");
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            return this.GetTickDamage(targets);
        }

        public float GetTickDamage(params Unit[] targets)
        {
            var target = targets.First();

            var damage = this.TickDamage;
            var amplify = this.Ability.SpellAmplification();
            var reduction = this.Ability.GetDamageReduction(target);

            return DamageHelpers.GetSpellDamage(damage, amplify, reduction);
        }

        public float GetTotalDamage(params Unit[] target)
        {
            return this.GetTickDamage(target) * (this.Duration / this.TickRate);
        }
    }
}