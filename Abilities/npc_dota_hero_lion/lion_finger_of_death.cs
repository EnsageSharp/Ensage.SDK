// <copyright file="lion_finger_of_death.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_lion
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class lion_finger_of_death : RangedAbility, IHasTargetModifier
    {
        // TODO: scepter
        public lion_finger_of_death(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_lion_finger_of_death";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "damage");
            }
        }
    }
}