// <copyright file="HeroExtensions.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using System.Reflection;

    

    using NLog;

    public static class HeroExtensions
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

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

        public static string GetDisplayName(this Unit unit)
        {
            var networkName = unit.NetworkName;
            switch (networkName)
            {
                case "CDOTA_Unit_Hero_DoomBringer":
                    return "Doom";
                case "CDOTA_Unit_Hero_Furion":
                    return "Nature's Prophet";
                case "CDOTA_Unit_Hero_Magnataur":
                    return "Magnus";
                case "CDOTA_Unit_Hero_Necrolyte":
                    return "Necrophos";
                case "CDOTA_Unit_Hero_Nevermore":
                    return "ShadowFiend";
                case "CDOTA_Unit_Hero_Obsidian_Destroyer":
                    return "OutworldDevourer";
                case "CDOTA_Unit_Hero_Rattletrap":
                    return "Clockwerk";
                case "CDOTA_Unit_Hero_Shredder":
                    return "Timbersaw";
                case "CDOTA_Unit_Hero_SkeletonKing":
                    return "WraithKing";
                case "CDOTA_Unit_Hero_Wisp":
                    return "Io";
                case "CDOTA_Unit_Hero_Zuus":
                    return "Zeus";
            }

            return networkName.Substring("CDOTA_Unit_Hero_".Length).Replace("_", string.Empty);
        }
    }
}