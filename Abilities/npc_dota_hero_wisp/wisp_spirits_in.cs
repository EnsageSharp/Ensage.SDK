// <copyright file="wisp_spirits_in.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_wisp
{
    public class wisp_spirits_in : ToggleAbility
    {
        public wisp_spirits_in(Ability ability)
            : base(ability)
        {
        }

        public override bool IsReady
        {
            get
            {
                return base.IsReady && this.Ability.IsActivated;
            }
        }
    }
}