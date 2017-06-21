// <copyright file="skywrath_mage_concussive_shot.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_skywrath_mage
{
    using Ensage.SDK.Extensions;

    public class skywrath_mage_concussive_shot : ActiveAbility, IAreaOfEffectAbility, IHasTargetModifier
    {
        public skywrath_mage_concussive_shot(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("launch_radius");
            }
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_skywrath_mage_concussive_shot_slow";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }
    }
}