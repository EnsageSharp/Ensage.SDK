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
            var data = ability.AbilitySpecialData.FirstOrDefault(x => x.Name == name);
            if (data == null)
            {
                return 0;
            }

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