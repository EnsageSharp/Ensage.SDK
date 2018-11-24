// <copyright file="centaur_return.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Abilities.npc_dota_hero_centaur
{
    using Ensage.SDK.Abilities.Components;
    using Ensage.SDK.Extensions;

    // Retailate ability, they didn't change the internal name.
    public class centaur_return : ActiveAbility, IAuraAbility, IHasModifier
    {
        public centaur_return(Ability ability)
            : base(ability)
        {
        }

        public string AuraModifierName { get; } = "modifier_centaur_return";

        public float AuraRadius
        {
            get
            {
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_centaur_3);
                if (talent?.Level > 0)
                {
                    return this.Ability.GetAbilitySpecialData("aura_radius");
                }

                return 0;
            }
        }

        public string ModifierName { get; } = "modifier_centaur_return";
    }
}