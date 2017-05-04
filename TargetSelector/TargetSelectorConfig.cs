// <copyright file="TargetSelectorConfig.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.TargetSelector
{
    using System.Reflection;

    using Ensage.Common.Menu;
    using Ensage.SDK.Menu;
    using Ensage.SDK.Service;

    using log4net;

    using PlaySharp.Toolkit.Logging;

    public class TargetSelectorConfig
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public TargetSelectorConfig(IServiceContext context, string[] modes)
        {
            this.Factory = MenuFactory.Create("Target Selector");
            this.Active = this.Factory.Item("Mode", new StringList(modes));

            context.Container.RegisterValue(this);
        }

        public MenuItem<StringList> Active { get; }

        public MenuFactory Factory { get; }

        public void UpdateModes(string[] @select)
        {
            Log.Debug(string.Join(", ", @select));
            this.Active.Value = new StringList(@select);
        }
    }
}