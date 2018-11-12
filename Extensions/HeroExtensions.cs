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

        public static string GetDisplayName(this Hero hero)
        {
            var heroId = hero.HeroId;
            switch (heroId)
            {
                case HeroId.npc_dota_hero_doom_bringer:
                    return "Doom";
                case HeroId.npc_dota_hero_furion:
                    return "Nature's Prophet";
                case HeroId.npc_dota_hero_magnataur:
                    return "Magnus";
                case HeroId.npc_dota_hero_necrolyte:
                    return "Necrophos";
                case HeroId.npc_dota_hero_nevermore:
                    return "ShadowFiend";
                case HeroId.npc_dota_hero_obsidian_destroyer:
                    return "OutworldDevourer";
                case HeroId.npc_dota_hero_rattletrap:
                    return "Clockwerk";
                case HeroId.npc_dota_hero_shredder:
                    return "Timbersaw";
                case HeroId.npc_dota_hero_skeleton_king:
                    return "WraithKing";
                case HeroId.npc_dota_hero_wisp:
                    return "Io";
                case HeroId.npc_dota_hero_zuus:
                    return "Zeus";
            }

            return heroId.ToString().Substring("npc_dota_hero_".Length).Replace("_", string.Empty);
        }
    }
}