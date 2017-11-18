// <copyright file="brewmaster_thunder_clap.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_brewmaster
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class brewmaster_thunder_clap : ActiveAbility, IAreaOfEffectAbility, IHasTargetModifier
    {
        public brewmaster_thunder_clap(Ability ability)
            : base(ability)
        {
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_brewmaster_thunder_clap";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }
    }
}