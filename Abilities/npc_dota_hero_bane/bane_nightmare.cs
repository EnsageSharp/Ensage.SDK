// <copyright file="bane_nightmare.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bane
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class bane_nightmare : RangedAbility, IHasTargetModifier
    {
        public bane_nightmare(Ability ability)
            : base(ability)
        {
            var end = this.Owner.GetAbilityById(AbilityId.bane_nightmare_end);
            this.EndAbility = new bane_nightmare_end(end);
        }

        public bane_nightmare_end EndAbility { get; }

        public override bool IsReady
        {
            get
            {
                return !this.Ability.IsHidden && base.IsReady;
            }
        }

        public string TargetModifierName { get; } = "modifier_bane_nightmare";
    }
}