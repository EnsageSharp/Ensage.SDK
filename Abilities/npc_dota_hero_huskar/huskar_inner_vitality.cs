// <copyright file="huskar_inner_vitality.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_huskar
{

    using Ensage.SDK.Extensions;

    public class huskar_inner_vitality : RangedAbility, IHasModifier
    {
        public huskar_inner_vitality(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName => "modifier_huskar_inner_vitality";
        public float BaseHeal => Ability.GetAbilitySpecialData("heal");
        public float AttributeBonusPercent => Ability.GetAbilitySpecialData("tooltip_attrib_bonus");
        public float HurtAttributeBonusPercent => Ability.GetAbilitySpecialData("tooltip_hurt_attrib_bonus");
        public float HurtPercent => Ability.GetAbilitySpecialData("hurt_percent") * 100;


    }
}
