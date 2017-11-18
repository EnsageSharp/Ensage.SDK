// <copyright file="bane_nightmare_end.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bane
{
    public class bane_nightmare_end : ActiveAbility
    {
        public bane_nightmare_end(Ability ability)
            : base(ability)
        {
        }

        public override bool IsReady
        {
            get
            {
                return !this.Ability.IsHidden && base.IsReady;
            }
        }
    }
}