// <copyright file="pangolier_swashbuckle.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_pangolier
{
    using System;

    using Ensage.SDK.Extensions;

    using SharpDX;

    public class pangolier_swashbuckle : LineAbility
    {
        public pangolier_swashbuckle(Ability ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return !this.Owner.IsRooted() && base.CanBeCasted;
            }
        }

        public override float Range
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("dash_range");
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("range");
            }
        }

        protected override string EndRadiusName { get; } = "end_radius";

        protected override string RadiusName { get; } = "start_radius";

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("damage");
                var strikes = this.Ability.GetAbilitySpecialData("strikes");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_pangolier_3);
                if (talent?.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage * strikes;
            }
        }

        protected override string SpeedName { get; } = "dash_speed";

        public override bool UseAbility(Unit target)
        {
            //todo fix after core update
            throw new NotImplementedException();
        }

        public override bool UseAbility(Vector3 position)
        {
            //todo fix after core update
            throw new NotImplementedException();
        }
    }
}