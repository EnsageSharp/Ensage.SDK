// <copyright file="arc_warden_spark_wraith.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_arc_warden
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class arc_warden_spark_wraith : CircleAbility, IHasTargetModifier
    {
        public arc_warden_spark_wraith(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("activation_delay");
            }
        }

        public string TargetModifierName { get; } = "modifier_arc_warden_spark_wraith_purge";

        public float WraithSpeed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("wraith_speed");
            }
        }

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("spark_damage");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_arc_warden);
                if (talent?.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }
    }
}