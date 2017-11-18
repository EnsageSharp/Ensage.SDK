// <copyright file="nyx_assassin_burrow.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_nyx_assassin
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class nyx_assassin_burrow : ActiveAbility, IHasModifier
    {
        public nyx_assassin_burrow(Ability ability)
            : base(ability)
        {
            var unburrowAbility = this.Owner.GetAbilityById(AbilityId.nyx_assassin_unburrow);
            this.UnburrowAbility = new nyx_assassin_unburrow(unburrowAbility);
        }

        public override bool IsReady
        {
            get
            {
                return !this.Ability.IsHidden && base.IsReady;
            }
        }

        public string ModifierName { get; } = "modifier_nyx_assassin_burrow";

        public nyx_assassin_unburrow UnburrowAbility { get; }
    }
}