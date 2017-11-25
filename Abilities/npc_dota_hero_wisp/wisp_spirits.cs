// <copyright file="wisp_spirits.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_wisp
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class wisp_spirits : ActiveAbility, IHasModifier, IAreaOfEffectAbility
    {
        public wisp_spirits(Ability ability)
            : base(ability)
        {
            var spiritsIn = this.Owner.GetAbilityById(AbilityId.wisp_spirits_in);
            var spiritsOut = this.Owner.GetAbilityById(AbilityId.wisp_spirits_out);
            this.SpiritsInAbility = new wisp_spirits_in(spiritsIn);
            this.SpiritsOutAbility = new wisp_spirits_out(spiritsOut);
        }

        public string ModifierName { get; } = "modifier_wisp_spirits";

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("hit_radius");
            }
        }

        public wisp_spirits_in SpiritsInAbility { get; }

        public float SpiritsMaxRange
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "max_range");
            }
        }

        public float SpiritsMinRange
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("min_range");
            }
        }

        public wisp_spirits_out SpiritsOutAbility { get; }

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "hero_damage");
            }
        }
    }
}