// <copyright file="wisp_spirits_out.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_wisp
{
    public class wisp_spirits_out : ToggleAbility
    {
        public wisp_spirits_out(Ability ability)
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