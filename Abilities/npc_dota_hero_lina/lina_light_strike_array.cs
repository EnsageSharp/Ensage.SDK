// <copyright file="lina_light_strike_array.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_lina
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class lina_light_strike_array : CircleAbility, IHasTargetModifierTexture
    {
        public lina_light_strike_array(Ability ability)
            : base(ability)
        {
        }

        public override float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("light_strike_array_aoe");
            }
        }

        public string[] TargetModifierTextureName { get; } = { "lina_light_strike_array" };

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "light_strike_array_damage");
            }
        }
    }
}