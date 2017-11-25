// <copyright file="pudge_meat_hook.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_pudge
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Prediction.Collision;

    public class pudge_meat_hook : LineAbility, IHasTargetModifier
    {
        public pudge_meat_hook(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override CollisionTypes CollisionTypes { get; } = CollisionTypes.AllUnits | CollisionTypes.Runes;

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("hook_width");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("hook_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_pudge_meat_hook";
    }
}