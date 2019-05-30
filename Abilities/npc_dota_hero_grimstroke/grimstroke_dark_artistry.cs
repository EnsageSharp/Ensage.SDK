// <copyright file="grimstroke_dark_artistry.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_grimstroke
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Prediction.Collision;

    public class grimstroke_dark_artistry : ConeAbility, IHasTargetModifier
    {
        public grimstroke_dark_artistry(Ability ability)
            : base(ability)
        {
        }


        public override CollisionTypes CollisionTypes { get; } = CollisionTypes.EnemyUnits;

        public override float EndRadius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("end_radius");
            }
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("start_radius");
            }
        }


        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("projectile_speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_grimstroke_dark_artistry_slow";
    }
}
