// <copyright file="chen_penitence.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_chen
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class chen_penitence : RangedAbility, IHasTargetModifier, IHasModifier
    {
        public chen_penitence(Ability ability)
            : base(ability)
        {
        }

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("speed");
            }
        }

        public string TargetModifierName { get; } = "modifier_chen_penitence";
        public string ModifierName { get; } = "modifier_chen_penitence_attack_speed_buff";
    }
}