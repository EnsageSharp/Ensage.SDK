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
                var damage = this.Ability.GetAbilitySpecialData("bonus_damage");

                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_ancient_apparition_2);
                if (talent?.Level > 0)
                {
                    damage += talent.GetAbilitySpecialData("value");
                }

                return damage;
            }
        }

        public string ModifierName { get; } = "modifier_chilling_touch";
    }
}