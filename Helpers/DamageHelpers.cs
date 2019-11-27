// <copyright file="DamageHelpers.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

using Ensage.Common.Extensions;

namespace Ensage.SDK.Helpers
{
    using Ensage.SDK.Extensions;

    public static class DamageHelpers
    {
        private static bool hasVeilDebuff = false;

        public static float GetDamageReduction(this Ability ability, Unit target)
        {
            return GetDamageReduction(ability, target, ability.DamageType);
        }

        public static float GetDamageReduction(this Ability ability, Unit target, DamageType damageType)
        {
            // TODO: modifiers
            if ((damageType & DamageType.HealthRemoval) == DamageType.HealthRemoval)
            {
                return 0.0f;
            }

            hasVeilDebuff = target.HasModifier("modifier_item_veil_of_discord_debuff"); // :broscience:

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

        /// <summary>
        ///     Gets the spell damage with the default values for calculation.
        /// </summary>
        /// <param name="damage">The raw damage of the spell.</param>
        /// <param name="amplify">The total spell amplification</param>
        /// <param name="reduction">The heroes reduction for the spell.</param>
        /// <returns>The total spell damage.</returns>
        public static float GetSpellDamage(float damage, float amplify = 0, float reduction = 0)
        {
            return hasVeilDebuff ? (damage * (1.0f + amplify) * (1.0f - reduction)) * 1.2f : damage * (1.0f + amplify) * (1.0f - reduction);
        }

        /// <summary>
        ///     Gets the spell damage with additional amplification values.
        /// </summary>
        /// <param name="damage">The raw damage of the spell.</param>
        /// <param name="modifiers">All modifiers including resistance (negative value) and amplification (positive value).</param>
        /// <returns>The total spell damage.</returns>
        public static float GetSpellDamage(float damage, params float[] modifiers)
        {
            var amplify = 1.0f;
            foreach (var modifier in modifiers)
            {
                amplify *= 1.0f + modifier;
            }

            return damage * amplify;
        }

        public static float SpellAmplification(this Ability ability)
        {
            var owner = ability.Owner as Unit;
            if (owner == null)
            {
                return 0.0f;
            }

            return owner.GetSpellAmplification();
        }
    }
}