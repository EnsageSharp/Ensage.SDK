// <copyright file="centaur_stampede.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_centaur
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class centaur_stampede : ActiveAbility, IHasModifier, IAreaOfEffectAbility
    {
        public centaur_stampede(Ability ability)
            : base(ability)
        {
        }

        public override DamageType DamageType { get; } = DamageType.Magical;

        public string ModifierName { get; } = "modifier_centaur_stampede";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        protected override float RawDamage
        {
            get
            {
                var strength = (this.Owner as Hero)?.TotalStrength ?? 0;
                var multiplier = this.Ability.GetAbilitySpecialData("strength_damage");

                return strength * multiplier;
            }
        }
    }
}