// <copyright file="AbilityExtensions.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class AbilityExtensions
    {
        public static readonly List<AbilityId> UniqueAttackModifiers =
            new List<AbilityId>
            {
                AbilityId.drow_ranger_frost_arrows,
                AbilityId.obsidian_destroyer_arcane_orb,
                AbilityId.silencer_glaives_of_wisdom,
                AbilityId.jakiro_liquid_fire,
                AbilityId.viper_poison_attack,
                AbilityId.enchantress_impetus,
                AbilityId.clinkz_searing_arrows,
                AbilityId.huskar_burning_spear
            };

        public static float GetAbilitySpecialData(this Ability ability, string name, uint level = 0)
        {
            var data = ability.AbilitySpecialData.First(x => x.Name == name);

            if (data.Count == 1)
            {
                return data.Value;
            }

            if (level == 0)
            {
                level = ability.Level;
            }

            return data.GetValue(level - 1);
        }
    }
}