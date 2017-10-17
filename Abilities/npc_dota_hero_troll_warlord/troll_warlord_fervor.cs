// <copyright file="troll_warlord_fervor.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_troll_warlord
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class troll_warlord_fervor : PassiveAbility, IHasModifier
    {
        public troll_warlord_fervor(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_troll_warlord_fervor";

        public float Charges
        {
            get
            {
                var modifier = this.Owner.GetModifierByName(this.ModifierName);
                return modifier?.StackCount ?? 0;
            }
        }
    }
}