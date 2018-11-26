// <copyright file="vengefulspirit_nether_swap.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_vengefulspirit
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Abilities.Components;

    public class vengefulspirit_nether_swap : RangedAbility, IHasModifier
    {
        public vengefulspirit_nether_swap(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_vengefulspirit_nether_swap_charge_counter";

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