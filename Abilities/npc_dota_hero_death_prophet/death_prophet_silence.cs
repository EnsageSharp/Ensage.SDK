// <copyright file="death_prophet_silence.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_death_prophet
{
    using Ensage.SDK.Abilities.Components;

    public class death_prophet_silence : CircleAbility, IHasTargetModifierTexture
    {
        public death_prophet_silence(Ability ability)
            : base(ability)
        {
        }

        public string[] TargetModifierTextureName { get; } = { "death_prophet_silence" };
    }
}