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

        public override bool CanBeCasted
        {
            get
            {
                return this.IsActivated && base.CanBeCasted;
            }
        }
    }
}