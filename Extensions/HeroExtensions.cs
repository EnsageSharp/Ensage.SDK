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
                var attackAnimationPoint = Game.FindKeyValues($"{hero.Name}/AttackAnimationPoint", KeyValueSource.Hero).FloatValue;

                return attackAnimationPoint / (1 + ((hero.AttackSpeedValue() - 100) / 100));
            }
            catch (KeyValuesNotFoundException)
            {
                Log.Warn($"Missing AttackAnimationPoint for {hero.Name}");
                return 0;
            }
        }

        public static string GetDisplayName(this Unit hero)
        {
            var classId = hero.ClassId;
            switch (classId)
            {
                case ClassId.CDOTA_Unit_Hero_DoomBringer:
                    return "Doom";
                case ClassId.CDOTA_Unit_Hero_Furion:
                    return "Nature's Prophet";
                case ClassId.CDOTA_Unit_Hero_Magnataur:
                    return "Magnus";
                case ClassId.CDOTA_Unit_Hero_Necrolyte:
                    return "Necrophos";
                case ClassId.CDOTA_Unit_Hero_Nevermore:
                    return "ShadowFiend";
                case ClassId.CDOTA_Unit_Hero_Obsidian_Destroyer:
                    return "OutworldDevourer";
                case ClassId.CDOTA_Unit_Hero_Rattletrap:
                    return "Clockwerk";
                case ClassId.CDOTA_Unit_Hero_Shredder:
                    return "Timbersaw";
                case ClassId.CDOTA_Unit_Hero_SkeletonKing:
                    return "WraithKing";
                case ClassId.CDOTA_Unit_Hero_Wisp:
                    return "Io";
                case ClassId.CDOTA_Unit_Hero_Zuus:
                    return "Zeus";
            }

            return classId.ToString().Substring("CDOTA_Unit_Hero_".Length).Replace("_", string.Empty);
        }
    }
}