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
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "damage");
            }
        }
    }
}