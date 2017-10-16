// <copyright file="troll_warlord_fervor.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_troll_warlord
{
    using Ensage.SDK.Extensions;

    public class troll_warlord_fervor : PassiveAbility
    {
        public troll_warlord_fervor(Ability ability)
            : base(ability)
        {
        }

        public string ChargeModifierName { get; } = "modifier_troll_warlord_fervor";

        public float Charges
        {
            get
            {
                var modifier = this.Owner.GetModifierByName(this.ChargeModifierName);
                return modifier?.StackCount ?? 0;
            }
        }
    }
}