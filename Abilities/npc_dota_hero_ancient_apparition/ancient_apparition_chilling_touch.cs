// <copyright file="ancient_apparition_chilling_touch.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_ancient_apparition
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class ancient_apparition_chilling_touch : CircleAbility, IHasModifier
    {
        public ancient_apparition_chilling_touch(Ability ability)
            : base(ability)
        {
        }

        public float BonusDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "bonus_damage");
            }
        }

        public string ModifierName { get; } = "modifier_chilling_touch";
    }
}