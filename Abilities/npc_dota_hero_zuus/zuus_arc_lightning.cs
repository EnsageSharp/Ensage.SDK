// <copyright file="zuus_arc_lightning.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_zuus
{
    public class zuus_arc_lightning : RangedAbility
    {
        public zuus_arc_lightning(Ability ability)
            : base(ability)
        {
        }
        
        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("#AbilityDamage");
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_zeus_2);
                if (talent != null && talent.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }
    }
}
