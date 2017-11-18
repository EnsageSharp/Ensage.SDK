// <copyright file="bounty_hunter_jinada.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bounty_hunter
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class bounty_hunter_jinada : PassiveAbility, IHasTargetModifier
    {
        public bounty_hunter_jinada(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_bounty_hunter_jinada_slow";

        protected override float RawDamage
        {
            get
            {
                var crit = this.Ability.GetAbilitySpecialData("crit_multiplier") / 100f;

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_bounty_hunter);
                if (talent?.Level > 0)
                {
                    crit += talent.GetAbilitySpecialData("value") / 100f;
                }

                return crit;
            }
        }

        public override float GetDamage(params Unit[] targets)
        {
            if (!targets.Any())
            {
                return 0;
            }

            return this.Owner.GetAttackDamage(targets.First()) * this.RawDamage;
        }
    }
}