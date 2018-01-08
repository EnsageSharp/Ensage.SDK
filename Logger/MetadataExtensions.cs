// <copyright file="MetadataExtensions.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Logger
{
    using System;
    using System.Linq;
    using System.Reflection;

    using PlaySharp.Toolkit.Helper.Annotations;

    public static class MetadataExtensions
    {
        [CanBeNull]
        public static AssemblyMetadata GetMetadata(this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var attributes = assembly.GetCustomAttributes<AssemblyMetadataAttribute>().ToArray();
            if (attributes.Length == 0)
            {
                return null;
            }

            var metadata = new AssemblyMetadata();

            foreach (var data in attributes)
            {
                try
                {
                    switch (data.Key)
                    {
                        case "Id":
                            metadata.Id = int.Parse(data.Value);
                            break;

                        case "Commit":
                            metadata.Commit = data.Value;
                            break;

                        case "Channel":
                            metadata.Channel = data.Value;
                            break;

                        case "Version":
                            metadata.Version = int.Parse(data.Value);
                            break;

                        case "Build":
                            metadata.Build = int.Parse(data.Value);
                            break;

                        case "SentryKey":
                            metadata.SentryKey = data.Value;
                            break;

                        case "SentryProject":
                            metadata.SentryProject = data.Value;
                            break;

                        default:
                            metadata.Extra[data.Key] = data.Value;
                            break;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"Assembly {assembly.FullName} contains an invalid {nameof(AssemblyMetadataAttribute)} {data.Key}={data.Value}", e);
                }
            }

            return metadata;
        }
    }
}