// <copyright file="CallbacksTable.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins.DebugTables
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    [Export(typeof(Table))]
    public sealed class CallbacksTable : Table
    {
        public CallbacksTable()
            : base("Callbacks")
        {
        }

        public override IReadOnlyList<TableColumn> OnUpdate()
        {
            var handlers = UpdateManager.Handlers.Where(e => e.Executor is TraceHandler).ToArray();
            var tracers = handlers.Select(e => e.Executor as TraceHandler).ToArray();

            return new[]
                   {
                       handlers.Select(e => e.Name).ToColumn("Name"),
                       tracers.Select(e => e.Time.Ticks.ToString("N0")).ToColumn("Ticks"),
                       tracers.Select(e => e.TimeHistory.Average(t => t.Ticks).ToString("N0")).ToColumn("Average", 20),
                       tracers.Select(e => e.TimeHistory.Min(t => t.Ticks).ToString("N0")).ToColumn("Min", 20),
                       tracers.Select(e => e.TimeHistory.Max(t => t.Ticks).ToString("N0")).ToColumn("Max", 20),
                       tracers.Select(e => e.Timeout.ToString()).ToColumn("Timeout")
                   };
        }
    }
}