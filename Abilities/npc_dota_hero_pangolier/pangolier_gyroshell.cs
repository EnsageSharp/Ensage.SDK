// <copyright file="pangolier_gyroshell.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_pangolier
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class pangolier_gyroshell : ActiveAbility, IAreaOfEffectAbility, IHasModifier, IHasTargetModifier
    {
        public pangolier_gyroshell(Ability ability)
            : base(ability)
        {
            var stopAbility = this.Owner.GetAbilityById(AbilityId.pangolier_gyroshell_stop);
            this.GyroshellStopAbility = new pangolier_gyroshell_stop(stopAbility);
        }

        public pangolier_gyroshell_stop GyroshellStopAbility { get; }

        public string ModifierName { get; } = "modifier_pangolier_gyroshell";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("hit_radius");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("forward_move_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_pangolier_gyroshell_stunned";
    }
}