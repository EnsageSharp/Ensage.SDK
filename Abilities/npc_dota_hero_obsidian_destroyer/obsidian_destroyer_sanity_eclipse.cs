// <copyright file="obsidian_destroyer_sanity_eclipse.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using System;
using Ensage.SDK.Prediction.Collision;

namespace Ensage.SDK.Abilities.npc_dota_hero_obsidian_destroyer
{

    using Ensage.SDK.Extensions;

    using Ensage.SDK.Prediction;


    public class obsidian_destroyer_sanity_eclipse : PredictionAbility
    {
        public obsidian_destroyer_sanity_eclipse(Ability ability)
            : base(ability)
        {
        }

        public override PredictionSkillshotType PredictionSkillshotType => PredictionSkillshotType.SkillshotCircle;
        public override float Speed => float.MaxValue;
    }
}
