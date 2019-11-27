// <copyright file="rattletrap_overclocking.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>
namespace Ensage.SDK.Abilities.npc_dota_hero_rattletrap
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    class rattletrap_overclocking : ActiveAbility
    {
        public rattletrap_overclocking(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_rattletrap_overclocking";

        public override float Duration
        {
            get
            {
                return Ability.GetAbilitySpecialData("buff_duration");
            }
        }
    }
}
