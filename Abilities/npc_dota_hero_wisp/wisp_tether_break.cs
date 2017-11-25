// <copyright file="wisp_tether_break.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_wisp
{
    public class wisp_tether_break : ActiveAbility
    {
        public wisp_tether_break(Ability ability)
            : base(ability)
        {
        }

        public override bool IsReady
        {
            get
            {
                return base.IsReady && !this.Ability.IsHidden;
            }
        }
    }
}