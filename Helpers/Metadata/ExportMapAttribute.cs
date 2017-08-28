namespace Ensage.SDK.Helpers.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportMapAttribute : ExportAttribute, IMapMetadata
    {
        public ExportMapAttribute(string name)
            : base(typeof(Map))
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}