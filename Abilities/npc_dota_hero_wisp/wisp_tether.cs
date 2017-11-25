// <copyright file="wisp_tether.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_wisp
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class wisp_tether : RangedAbility, IHasModifier, IHasTargetModifier, IAreaOfEffectAbility
    {
        public wisp_tether(Ability ability)
            : base(ability)
        {
            var tetherBreak = this.Owner.GetAbilityById(AbilityId.wisp_tether_break);
            this.TetherBreakAbility = new wisp_tether_break(tetherBreak);
        }

        public bool IsAghanimsBonusActive
        {
            get
            {
                return this.Owner.GetAbilityById(AbilityId.special_bonus_unique_wisp_5)?.Level > 0;
            }
        }

        public override bool IsReady
        {
            get
            {
                return base.IsReady && !this.Ability.IsHidden;
            }
        }

        public string ModifierName { get; } = "modifier_wisp_tether";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("radius");
            }
        }

        public string TargetModifierName { get; } = "modifier_wisp_tether_haste";

        public wisp_tether_break TetherBreakAbility { get; }

        public override bool UseAbility()
        {
            return this.TetherBreakAbility.UseAbility();
        }
    }
}