// <copyright file="kunkka_return.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_kunkka
{
    public class kunkka_return : ActiveAbility
    {
        public kunkka_return(Ability ability)
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