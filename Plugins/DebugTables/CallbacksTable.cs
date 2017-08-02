// <copyright file="CallbacksTable.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins.DebugTables
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    using PlaySharp.Toolkit.Helper.Annotations;

    [Export(typeof(Table))]
    public sealed class CallbacksTable : Table
    {
        internal override void OnUpdate()
        {
            var handlers = UpdateManager.Handlers.Where(e => e.Executor is TraceHandler).ToArray();
            var tracers = handlers.Select(e => e.Executor as TraceHandler).ToArray();
            var list = new List<TableColumn>
                       {
                           new TableColumn("Name", handlers.Select(e => e.Name)),
                           new TableColumn("Ticks", 20, tracers.Select(e => e.Time.Ticks.ToString("N0"))),
                           new TableColumn("Average", 20, tracers.Select(e => e.TimeHistory.Average(t => t.Ticks).ToString("N0"))),
                           new TableColumn("Min", 20, tracers.Select(e => e.TimeHistory.Min(t => t.Ticks).ToString("N0"))),
                           new TableColumn("Max", 20, tracers.Select(e => e.TimeHistory.Max(t => t.Ticks).ToString("N0"))),
                           new TableColumn("Timeout", tracers.Select(e => e?.Timeout.ToString()))
                       };

            this.Columns = list;
        }

        public CallbacksTable()
            : base("Callbacks")
        {
        }
    }
}