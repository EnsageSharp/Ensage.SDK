// <copyright file="ExportAssemblyAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportAssemblyAttribute : ExportAttribute, IAssemblyLoaderMetadata
    {
        public ExportAssemblyAttribute(string name, params HeroId[] units)
            : this(name, null, null, units)
        {
        }

        public ExportAssemblyAttribute(string name, string author, params HeroId[] units)
            : this(name, author, null, units)
        {
        }

        public ExportAssemblyAttribute(string name, string author, string version, HeroId[] units)
            : base(typeof(IAssemblyLoader))
        {
            this.Name = name;
            this.Author = author;
            this.Version = version;
            this.Units = units.Length > 0 ? units : null;
        }

        [DefaultValue("Ensage")]
        public string Author { get; }

        public string Name { get; }

        [DefaultValue(null)]
        public HeroId[] Units { get; }

        [DefaultValue("1.0.0.0")]
        public string Version { get; }
    }
}