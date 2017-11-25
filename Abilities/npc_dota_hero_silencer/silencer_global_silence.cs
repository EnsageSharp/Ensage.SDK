// <copyright file="silencer_global_silence.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_silencer
{
    using Ensage.SDK.Abilities.Components;

    public class silencer_global_silence : ActiveAbility, IHasTargetModifier, IAreaOfEffectAbility
    {
        public silencer_global_silence(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Silenced;

        public float Radius { get; } = float.MaxValue;

        public string TargetModifierName { get; } = "modifier_silencer_global_silence";
    }
}