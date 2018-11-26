// <copyright file="keeper_of_the_light_chakra_magic.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_keeper_of_the_light
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    public class keeper_of_the_light_chakra_magic : RangedAbility, IHasTargetModifier, IHasManaRestore
    {
        public keeper_of_the_light_chakra_magic(Ability ability)
            : base(ability)
        {
        }

        public string TargetModifierName { get; } = "modifier_keeper_of_the_light_chakra_magic";

        public float TotalManaRestore
        {
            get
            {
                return this.Ability.GetAbilitySpecialDataWithTalent(this.Owner, "mana_restore");
            }
        }

        public float CooldownReduction
        {
            get
            {
                return Ability.GetAbilitySpecialData("cooldown_reduction");
            }
        }
    }
}