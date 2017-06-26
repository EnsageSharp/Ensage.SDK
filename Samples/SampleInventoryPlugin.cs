// <copyright file="SampleInventoryPlugin.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Samples
{
    using System.ComponentModel.Composition;
    using System.Reflection;

    using Ensage.SDK.Abilities.Items;
    using Ensage.SDK.Inventory;
    using Ensage.SDK.Inventory.Metadata;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    [ExportPlugin("SampleInventoryPlugin", StartupMode.Manual)]
    public class SampleInventoryPlugin : Plugin
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ImportingConstructor]
        public SampleInventoryPlugin([Import] IServiceContext context, [Import] IInventoryManager inventory)
        {
            this.Context = context;
            this.Inventory = inventory;
        }

        [ItemBinding]
        public item_arcane_boots Boots { get; set; }

        public IServiceContext Context { get; }

        [ItemBinding]
        public item_dagon Dagon { get; set; }

        public IInventoryManager Inventory { get; }

        protected override void OnActivate()
        {
            this.Inventory.Attach(this);
        }
    }
}