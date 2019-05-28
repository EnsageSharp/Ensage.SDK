// <copyright file="tinker_laser.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_tinker
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;
    public class tinker_laser : RangedAbility, IHasTargetModifier
    {
        public tinker_laser(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_tinker_laser_blind";

        public override DamageType DamageType { get; } = DamageType.Pure;

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("laser_damage");
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_tinker);
                if (talent?.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }
    }
}
