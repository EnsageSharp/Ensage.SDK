// <copyright file="ExportInventoryManagerAttribute.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Inventory.Metadata
{
    using System;
    using System.ComponentModel.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportInventoryManagerAttribute : ExportAttribute, IInventoryManagerMetadata
    {
        public ExportInventoryManagerAttribute()
            : base(typeof(IInventoryManager))
        {
        }
    }
}