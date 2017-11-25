// <copyright file="puck_ethereal_jaunt.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_puck
{
    public class puck_ethereal_jaunt : ActiveAbility
    {
        public puck_ethereal_jaunt(Ability ability)
            : base(ability)
        {
        }

        public override bool IsReady
        {
            get
            {
                return this.Ability.IsActivated && base.IsReady;
            }
        }
    }
}