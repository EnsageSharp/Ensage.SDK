// <copyright file="tidehunter_gush.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_tidehunter
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class tidehunter_gush : RangedAbility, IHasTargetModifier
    {
        // TODO: scepter
        public tidehunter_gush(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_tidehunter_gush";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "gush_damage");
            }
        }
    }
}