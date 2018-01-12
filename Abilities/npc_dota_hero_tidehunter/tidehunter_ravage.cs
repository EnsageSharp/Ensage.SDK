// <copyright file="tidehunter_ravage.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_tidehunter
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class tidehunter_ravage : AreaOfEffectAbility, IHasTargetModifier
    {
        public tidehunter_ravage(Ability ability)
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

        public string TargetModifierName { get; } = "modifier_tidehunter_ravage";
    }
}