// <copyright file="DamageHelpers.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System.Linq;

    using Ensage.SDK.Extensions;

    public static class DamageHelpers
    {
        public static float GetDamageReduction(this Ability ability, Unit target)
        {
            var damageType = ability.DamageType;

            if (damageType.HasFlag(DamageType.HealthRemoval))
            {
                return 0.0f;
            }

            var reduction = 0.0f;
            switch (damageType)
            {
                case DamageType.Magical:
                    reduction = target.MagicDamageResist;
                    break;
                case DamageType.Physical:
                    reduction = target.DamageResist;
                    break;
            }

            return reduction;
        }

        public static float SpellAmplification(this Ability ability)
        {
            var owner = ability.Owner as Unit;
            if (owner == null)
            {
                return 0.0f;
            }

            var spellAmp = 0.0f;

            var hero = owner as Hero;
            if (hero != null)
            {
                spellAmp += hero.TotalIntelligence / 16.0f / 100.0f;
            }

            var aether = owner.GetItemById(AbilityId.item_aether_lens);
            if (aether != null)
            {
                spellAmp += aether.AbilitySpecialData.First(x => x.Name == "spell_amp").Value / 100.0f;
            }

            var talents =
                owner.Spellbook.Spells.Where(x => x.Level > 0 && x.Name.StartsWith("special_bonus_spell_amplify_"));
            foreach (var talent in talents)
            {
                spellAmp += talent.AbilitySpecialData.First(x => x.Name == "value").Value / 100.0f;
            }

            return spellAmp;
        }

        public static float GetSpellDamage(float damage, float amplify, float reduction)
        {
            return damage * (1.0f + amplify) * (1.0f - reduction);
        }
    }
}