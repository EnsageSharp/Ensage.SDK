// <copyright file="pangolier_shield_crash.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_pangolier
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class pangolier_shield_crash : ActiveAbility, IHasModifier, IAreaOfEffectAbility
    {
        public pangolier_shield_crash(Ability ability)
            : base(ability)
        {
        }

        public string ModifierName { get; } = "modifier_pangolier_shield_crash_buff";

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
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }
    }
}