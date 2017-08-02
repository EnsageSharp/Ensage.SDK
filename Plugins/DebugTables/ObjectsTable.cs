// <copyright file="ObjectsTable.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins.DebugTables
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Helpers;

    [Export(typeof(Table))]
    public sealed class ObjectsTable : Table
    {
        public ObjectsTable()
            : base("Units")
        {
        }

        internal override void OnUpdate()
        {
            var data = EntityManager<Unit>.Entities.Where(e => !e.UnitState.HasFlag(UnitState.FakeAlly) && e.MaximumHealth > 0)
                                          .OrderByDescending(e => e is Hero)
                                          .ThenBy(e => e.Team)
                                          .ToArray();

            var list = new List<TableColumn>
                       {
                           new TableColumn("Name", data.Select(e => e.Name)),
                           new TableColumn("Health", data.Select(e => $"{e.Health:n0} / {e.MaximumHealth:n0}")),
                           new TableColumn("Mana", data.Select(e => $"{e.Mana:n0} / {e.MaximumMana:n0}")),
                           new TableColumn("Animation", 200, data.Select(e => e.Animation.Name)),
                           new TableColumn("State", data.Select(e => e.UnitState.ToString())),
                       };

            this.Columns = list;
        }
    }
}