// <copyright file="elder_titan_return_spirit.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_elder_titan
{
    public class elder_titan_return_spirit : ActiveAbility
    {
        public elder_titan_return_spirit(Ability ability)
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