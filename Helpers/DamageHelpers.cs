// <copyright file="DamageHelpers.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using Ensage.SDK.Extensions;

    public static class DamageHelpers
    {
        public static float GetDamageReduction(this Ability ability, Unit target)
        {
            // TODO: modifiers
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

        public static float GetSpellDamage(float damage, float amplify, float reduction)
        {
            return damage * (1.0f + amplify) * (1.0f - reduction);
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