// <copyright file="lion_impale.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_lion
{
    using System.Linq;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class lion_impale : LineAbility, IHasTargetModifier
    {
        public lion_impale(Ability ability)
            : base(ability)
        {
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("width");
            }
        }

        public override float Range
        {
            get
            {
                var range = this.Ability.GetAbilitySpecialData("length_buffer");
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_lion_2);
                if (talent != null && talent.Level > 0)
                {
                    range += talent.AbilitySpecialData.First(x => x.Name == "value").Value;
                }

                return range;
            }
        }

        public string TargetModifierName { get; } = "modifier_lion_impale";
    }
}