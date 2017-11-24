// <copyright file="LocalizeHelper.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Localization;

    using PlaySharp.Toolkit.Helper.Annotations;

    [PublicAPI]
    public static class LocalizationHelper
    {
        public static string Localize(Loc localization, params object[] objects)
        {
            var text = localization.ToEnumString();

            var match = Regex.Matches(text, @"(?<!\{)\{([0-9]+).*?\}(?!})").Cast<Match>().ToList();
            if (match.Any())
            {
                var count = match.Max(m => int.Parse(m.Groups[1].Value)) + 1;
                if (count != objects.Length)
                {
                    throw new ArgumentException($"Given {objects.Length} arguments, but {count} arguments were expected");
                }

                if (count > 0)
                {
                    text = string.Format(text, objects);
                }
            }

            return Game.Localize(text);
        }

        public static string LocalizeName(Ability ability)
        {
            return Localize(Loc.DOTA_Tooltip_Ability_STRING, ability.Name);
        }

        public static string LocalizeName(Entity entity)
        {
            return Game.Localize($"{entity.Name}");
        }
    }
}