// <copyright file="keeper_of_the_light_spirit_form_illuminate_end.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_keeper_of_the_light
{
    public class keeper_of_the_light_spirit_form_illuminate_end : ActiveAbility
    {
        public keeper_of_the_light_spirit_form_illuminate_end(Ability ability)
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