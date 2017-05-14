// <copyright file="MetadataExtensions.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Extensions
{
    using System.Linq;

    using Ensage.SDK.Service.Metadata;

    public static class MetadataExtensions
    {
        public static bool IsSupported(this IPluginLoaderMetadata metadata)
        {
            // unit filter
            if (metadata.Units != null)
            {
                if (metadata.Units.Contains(ObjectManager.LocalHero.HeroId))
                {
                    return true;
                }

                return false;
            }

            return true;
        }
    }
}