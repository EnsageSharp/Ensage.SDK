// <copyright file="chaos_knight_chaos_bolt.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_chaos_knight
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class chaos_knight_chaos_bolt : RangedAbility, IHasTargetModifierTexture
    {
        public chaos_knight_chaos_bolt(Ability ability)
            : base(ability)
        {
        }

        public override UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float Speed
        {
            get
            {
                return this.Ability.GetAbilitySpecialData("chaos_bolt_speed");
            }
        }

        public string[] TargetModifierTextureName { get; } = { "chaos_knight_chaos_bolt" };

        protected override float RawDamage
        {
            get
            {
                // lets take average damage
                return (this.Ability.GetAbilitySpecialData("damage_min") + this.Ability.GetAbilitySpecialData("damage_max")) / 2;
            }
        }
    }
}