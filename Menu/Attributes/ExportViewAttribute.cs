// <copyright file="ExportViewAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu.Attributes
{
    using System;
    using System.ComponentModel.Composition;

    using Ensage.SDK.Menu.Views;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportViewAttribute : ExportAttribute, IViewMetadata
    {
        public ExportViewAttribute(Type target)
            : base(typeof(View))
        {
            this.Target = target;
        }

        public Type Target { get; }
    }
}