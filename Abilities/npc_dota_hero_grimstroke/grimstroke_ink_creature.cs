// <copyright file="grimstroke_ink_creature.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_grimstroke
{
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Abilities.Components;

    public class grimstroke_ink_creature : RangedAbility, IHasTargetModifier
    {
        public grimstroke_ink_creature(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_grimstroke_ink_creature_debuff";

        public override float ActivationDelay
        {
            get
            {
                return Ability.GetAbilitySpecialData("latch_duration");
            }
        }
    }
}
