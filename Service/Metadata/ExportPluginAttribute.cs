// <copyright file="ExportPluginAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportPluginAttribute : ExportAttribute, IPluginLoaderMetadata
    {
        public ExportPluginAttribute(
            string name,
            StartupMode mode = StartupMode.Auto,
            string author = "Ensage",
            string version = "1.0.0.0",
            string description = "",
            params HeroId[] units)
            : base(typeof(IPluginLoader))
        {
            this.Name = name;
            this.Mode = mode;
            this.Author = author;
            this.Version = version;
            this.Description = description;
            this.Units = units?.Length > 0 ? units : null;
        }

        public ExportPluginAttribute(string name, params HeroId[] units)
            : this(name, StartupMode.Auto, units: units)
        {
        }

        public string Author { get; }

        public string Description { get; }

        public StartupMode Mode { get; }

        public string Name { get; }

        public HeroId[] Units { get; }

        public string Version { get; }
    }
}