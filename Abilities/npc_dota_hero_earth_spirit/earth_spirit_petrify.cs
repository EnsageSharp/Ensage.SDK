// <copyright file="earth_spirit_petrify.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_earth_spirit
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class earth_spirit_petrify : RangedAbility, IHasTargetModifier
    {
        public earth_spirit_petrify(Ability ability)
            : base(ability)
        {
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public override bool IsReady
        {
            get
            {
                return base.IsReady && !this.Ability.IsHidden;
            }
        }

        public string TargetModifierName { get; } = "modifier_earthspirit_petrify";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("damage");
            }
        }
    }
}