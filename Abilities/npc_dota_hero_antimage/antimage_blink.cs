// <copyright file="antimage_blink.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_antimage
{
    using Ensage.SDK.Extensions;

    public class antimage_blink : RangedAbility
    {
        public antimage_blink(Ability ability)
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

        public override float CastRange
        {
            get
            {
                var range = this.Ability.GetAbilitySpecialData("blink_range");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_antimage_3);
                if (talent?.Level > 0)
                {
                    range += talent.GetAbilitySpecialData("value");
                }

                return range;
            }
        }
        
        
    }
}
