// <copyright file="lion_finger_of_death.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_lion
{
    using System.Linq;

    using Ensage.Common.Extensions;

    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class lion_finger_of_death : RangedAbility, IHasTargetModifier, IAreaOfEffectAbility, IHasModifier
    {
        public lion_finger_of_death(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_lion_finger_of_death";
        public string TargetDelayModifierName { get; } = "modifier_lion_finger_of_death_delay";

        protected override float RawDamage
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "damage") + ExtraDamage;
            }
        }

        public float KillCounter
        {
            get
            {
                var modifier = this.Owner.GetModifierByName(this.ModifierName);
                return modifier?.StackCount ?? 0;
            }
        }

        public float ExtraDamage
        {
            get
            {
                return KillCounter * this.Ability.GetAbilitySpecialData("damage_per_kill");
            }
        }

        public float Radius
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("splash_radius_scepter");
            }
        }

        public string ModifierName { get; } = "modifier_lion_finger_of_death_kill_counter";
    }
}