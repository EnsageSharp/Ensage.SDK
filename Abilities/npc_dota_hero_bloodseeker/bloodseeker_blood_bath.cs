// <copyright file="bloodseeker_blood_bath.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_bloodseeker
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class bloodseeker_blood_bath : CircleAbility, IHasTargetModifierTexture
    {
        public bloodseeker_blood_bath(Ability ability)
            : base(ability)
        {
        }

        public override float ActivationDelay
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("delay");
            }
        }

        public string[] TargetModifierTextureName { get; set; } = { "bloodseeker_blood_bath" };

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "damage");
            }
        }
    }
}