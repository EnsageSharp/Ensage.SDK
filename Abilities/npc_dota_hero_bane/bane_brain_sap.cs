// <copyright file="bane_brain_sap.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bane
{
    using Ensage.SDK.Extensions;

    public class bane_brain_sap : RangedAbility
    {
        public bane_brain_sap(Ability ability)
            : base(ability)
        {
        }

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("brain_sap_damage");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_bane_2);
                if (talent?.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }
    }
}