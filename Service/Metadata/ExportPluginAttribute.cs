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
        public ExportPluginAttribute(string name, params HeroId[] units)
            : this(name, "Ensage", "1.0.0.0", StartupMode.Auto, units)
        {
        }

        public ExportPluginAttribute(string name, StartupMode mode, params HeroId[] units)
            : this(name, "Ensage", "1.0.0.0", mode, units)
        {
        }

        public ExportPluginAttribute(string name, string author, string version, StartupMode mode, params HeroId[] units)
            : base(typeof(IPluginLoader))
        {
            this.Name = name;
            this.Author = author;
            this.Version = version;
            this.Mode = mode;
            this.Units = units?.Length > 0 ? units : null;
        }

        public string Author { get; }

        public StartupMode Mode { get; }

        public string Name { get; }

        public HeroId[] Units { get; }

        public string Version { get; }
    }
}