// <copyright file="magnataur_skewer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_magnataur
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class magnataur_skewer : LineAbility, IHasTargetModifier, IHasModifier
    {
        public magnataur_skewer(Ability ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return base.CanBeCasted && !this.Owner.IsRooted();
            }
        }

        public string ModifierName { get; } = "modifier_magnataur_skewer_movement";

        public string TargetModifierName { get; } = "modifier_magnataur_skewer_impact";

        protected override float BaseCastRange
        {
            get
            {
                var castRange = this.Ability.GetAbilitySpecialData("range");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_magnus_3);
                if (talent != null && talent.Level > 0)
                {
                    castRange += talent.GetAbilitySpecialData("value");
                }

                return castRange;
            }
        }

        protected override string RadiusName { get; } = "skewer_radius";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("skewer_damage");
            }
        }

        protected override string SpeedName { get; } = "skewer_speed";
    }
}