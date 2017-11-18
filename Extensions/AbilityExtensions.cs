// <copyright file="AbilityExtensions.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using System.Linq;

    public static class AbilityExtensions
    {
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

        public static float GetAbilitySpecialDataWithTalent(this Ability ability, Unit owner, string name, uint level = 0)
        {
            var data = ability.AbilitySpecialData.First(x => x.Name == name);

            var talent = owner.GetAbilityById(data.SpecialBonusAbility);
            var talentValue = 0f;
            if (talent?.Level > 0)
            {
                talentValue = talent.AbilitySpecialData.First(x => x.Name == "value").Value;
            }

            if (data.Count == 1)
            {
                return data.Value + talentValue;
            }

            if (level == 0)
            {
                level = ability.Level;
            }

            if (level == 0)
            {
                return 0;
            }

            return data.GetValue(level - 1) + talentValue;
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
            var aetherLens = owner.GetItemById(AbilityId.item_aether_lens);
            if (aetherLens != null)
            {
                castRange += aetherLens.GetAbilitySpecialData("cast_range_bonus");
            }

            // talents
            var talent = owner.Spellbook.Spells.FirstOrDefault(x => x.Level > 0 && x.Name.StartsWith("special_bonus_cast_range_"));
            if (talent != null)
            {
                castRange += talent.GetAbilitySpecialData("value");
            }

            return castRange;
        }
    }
}