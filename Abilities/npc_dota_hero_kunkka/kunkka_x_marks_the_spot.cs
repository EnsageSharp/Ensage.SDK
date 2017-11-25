// <copyright file="kunkka_x_marks_the_spot.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_kunkka
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class kunkka_x_marks_the_spot : RangedAbility, IHasTargetModifier
    {
        public kunkka_x_marks_the_spot(Ability ability)
            : base(ability)
        {
            var @return = this.Owner.GetAbilityById(AbilityId.kunkka_return);
            this.ReturnAbility = new kunkka_return(@return);
        }

        public override bool IsReady
        {
            get
            {
                return base.IsReady && !this.Ability.IsHidden;
            }
        }

        public kunkka_return ReturnAbility { get; }

        public string TargetModifierName { get; } = "modifier_kunkka_x_marks_the_spot";

        public override bool UseAbility()
        {
            return this.ReturnAbility.UseAbility();
        }
    }
}