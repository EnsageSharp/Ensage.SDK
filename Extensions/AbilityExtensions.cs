// <copyright file="AbilityExtensions.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class AbilityExtensions
    {
        public static readonly List<AbilityId> UniqueAttackModifiers = new List<AbilityId>
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

            if (level == 0)
            {
                return 0;
            }

            return data.GetValue(level - 1);
        }

        public static float GetCastPoint(this Ability ability)
        {
            if (ability is Item)
            {
                return 0;
            }

            var level = ability.Level;
            if (level == 0)
            {
                return 0;
            }

            if (ability.OverrideCastPoint != -1)
            {
                return 0.1f;
            }

            return ability.GetCastPoint(level - 1);
        }

        public static float GetCastRange(this Ability ability)
        {
            var castRange = (float)ability.CastRange;
            var owner = (Unit)ability.Owner;

            // items
            var aetherLense = owner.GetItemById(AbilityId.item_aether_lens);
            if (aetherLense != null)
            {
                castRange += aetherLense.GetAbilitySpecialData("cast_range_bonus");
            }

            // talents
            foreach (var talent in owner.Spellbook.Spells.Where(x => x.Level > 0 && x.Name.StartsWith("special_bonus_cast_range_")))
            {
                castRange += talent.GetAbilitySpecialData("value");
            }

            return castRange;
        }
    }
}