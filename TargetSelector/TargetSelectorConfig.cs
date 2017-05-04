// <copyright file="TargetSelectorConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Ensage.Common.Menu;
    using Ensage.SDK.Menu;
    using Ensage.SDK.TargetSelector.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public class TargetSelectorConfig : IPartImportsSatisfiedNotification
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public TargetSelectorConfig()
        {
            this.Factory = MenuFactory.Create("Target Selector");
        }

        public MenuItem<StringList> Active { get; private set; }

        public MenuFactory Factory { get; }

        [ImportManyTargetSelector]
        protected IEnumerable<Lazy<ITargetSelector, ITargetSelectorMetadata>> Selectors { get; set; }

        public void OnImportsSatisfied()
        {
            var modes = this.Selectors.Select(e => e.Metadata.Name).ToArray();
            Log.Debug($"Resolved Modes: {string.Join(" - ", modes)}");

            if (this.Active == null)
            {
                this.Active = this.Factory.Item("Mode", new StringList(modes));
            }
            else
            {
                this.Active.Value = new StringList(modes);
            }
        }
    }
}