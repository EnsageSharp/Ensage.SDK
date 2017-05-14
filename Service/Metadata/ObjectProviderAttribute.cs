// <copyright file="ObjectProviderAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Service.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [AttributeUsage(AttributeTargets.Class)]
    public class ObjectProviderAttribute : ExportAttribute, IServiceMetadata
    {
        protected ObjectProviderAttribute(Type contract, string name, string version = null, string description = null)
            : base(contract)
        {
            this.Name = name;
            this.Version = version;
            this.Description = description;
        }

        public string Description { get; }

        public string Name { get; }

        public string Version { get; }
    }
}