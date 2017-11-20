// <copyright file="earth_spirit_stone_caller.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_earth_spirit
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class earth_spirit_stone_caller : RangedAbility, IHasModifier
    {
        public earth_spirit_stone_caller(Ability ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted
        {
            get
            {
                return base.CanBeCasted && this.Owner.GetModifierByName(this.ModifierName)?.StackCount > 0;
            }
        }

        public override float Duration
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("duration");
            }
        }

        public string ModifierName { get; } = "modifier_earth_spirit_stone_caller_charge_counter";
    }
}