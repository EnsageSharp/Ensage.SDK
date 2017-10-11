// <copyright file="broodmother_spawn_spiderlings.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_broodmother
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    using PlaySharp.Toolkit.Helper.Annotations;

    public class broodmother_spawn_spiderlings : RangedAbility, IHasTargetModifier
    {
        public broodmother_spawn_spiderlings([NotNull] Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_broodmother_spawn_spiderlings";

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("damage");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_broodmother_3);
                if (talent != null && talent.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }
    }
}