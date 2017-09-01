// <copyright file="queenofpain_blink.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_queenofpain
{
    using Ensage.SDK.Extensions;

    public class queenofpain_blink : RangedAbility
    {
        public queenofpain_blink(Ability ability)
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

        protected override float BaseCastRange
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("blink_range");
            }
        }
    }
}