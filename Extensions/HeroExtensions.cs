// <copyright file="HeroExtensions.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using System.Reflection;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public static class HeroExtensions
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static float AttackPoint(this Hero hero)
        {
            try
            {
                var attackAnimationPoint =
                    Game.FindKeyValues($"{hero.Name}/AttackAnimationPoint", KeyValueSource.Hero).FloatValue;

                return attackAnimationPoint / (1 + (hero.AttackSpeedValue() - 100) / 100);
            }
            catch (KeyValuesNotFoundException)
            {
                Log.Warn($"Missing AttackAnimationPoint for {hero.Name}");
                return 0;
            }
        }
    }
}