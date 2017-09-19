// <copyright file="silencer_global_silence.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_silencer
{
    public class silencer_global_silence : ActiveAbility, IHasTargetModifier
    {
        public silencer_global_silence(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_silencer_global_silence";
    }
}