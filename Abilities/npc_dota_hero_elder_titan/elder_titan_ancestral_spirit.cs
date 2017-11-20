// <copyright file="elder_titan_ancestral_spirit.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_elder_titan
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class elder_titan_ancestral_spirit : CircleAbility, IHasModifier
    {
        public elder_titan_ancestral_spirit(Ability ability)
            : base(ability)
        {
            var returnAbility = this.Owner.GetAbilityById(AbilityId.elder_titan_return_spirit);
            this.ReturnAbility = new elder_titan_return_spirit(returnAbility);
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("spirit_duration");
            }
        }

        public override bool IsReady
        {
            get
            {
                return base.IsReady && !this.Ability.IsHidden;
            }
        }

        public string ModifierName { get; } = "modifier_elder_titan_ancestral_spirit_buff";

        public elder_titan_return_spirit ReturnAbility { get; }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("pass_damage");
            }
        }

        public override bool UseAbility()
        {
            return this.ReturnAbility.CanBeCasted && this.ReturnAbility.UseAbility();
        }
    }
}