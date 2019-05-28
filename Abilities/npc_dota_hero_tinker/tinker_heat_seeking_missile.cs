// <copyright file="tinker_heat_seeking_missile.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_tinker
{
    using Ensage.SDK.Extensions;

    public class tinker_heat_seeking_missile : ActiveAbility
    {
        public tinker_heat_seeking_missile(Ability ability)
            : base(ability)
        {
        }

        public override DamageType DamageType { get; } = DamageType.Magical;

        protected override float RawDamage
        {
            get
            {
                var damage = this.Ability.GetAbilitySpecialData("damage");
                return damage;
            }
        }
        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("speed");
            }
        }
    }

}
