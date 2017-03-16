using System;
using System.ComponentModel.Composition;
using Ensage.SDK.Service;

namespace Ensage.SDK.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ObjectProviderAttribute : ExportAttribute, IServiceMetadata
    {
        protected ObjectProviderAttribute(Type contract, string name, string version = null, string description = null)
            : base(contract)
        {
            Name = name;
            Version = version;
            Description = description;
        }

        public string Description { get; }

        public string Name { get; }

        public string Version { get; }
    }
}