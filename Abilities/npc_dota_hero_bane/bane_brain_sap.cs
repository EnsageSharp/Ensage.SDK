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
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "brain_sap_damage");
            }
        }
    }
}