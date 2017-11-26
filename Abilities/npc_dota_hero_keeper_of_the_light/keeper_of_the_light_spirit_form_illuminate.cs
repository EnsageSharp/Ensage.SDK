// <copyright file="keeper_of_the_light_spirit_form_illuminate.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_keeper_of_the_light
{
    using Ensage.SDK.Extensions;

    public class keeper_of_the_light_spirit_form_illuminate : LineAbility
    {
        public keeper_of_the_light_spirit_form_illuminate(Ability ability)
            : base(ability)
        {
            var illuminateEnd = this.Owner.GetAbilityById(AbilityId.keeper_of_the_light_spirit_form_illuminate_end);
            this.IlluminateEndAbility = new keeper_of_the_light_spirit_form_illuminate_end(illuminateEnd);
        }

        public keeper_of_the_light_spirit_form_illuminate_end IlluminateEndAbility { get; }

        public override bool IsReady
        {
            get
            {
                return base.IsReady && !this.Ability.IsHidden;
            }
        }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "total_damage");
            }
        }

        public override bool UseAbility()
        {
            return this.IlluminateEndAbility.UseAbility();
        }
    }
}