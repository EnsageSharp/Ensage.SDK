// <copyright file="sleight_of_fist.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_ember_spirit
{
    public class ember_spirit_sleight_of_fist : CircleAbility, IHasModifier
    {
        public ember_spirit_sleight_of_fist(Ability ability)
            : base(ability)
        {
        }
        
        public string ModifierName { get; } = "modifier_ember_spirit_sleight_of_fist_caster";
    }

}
