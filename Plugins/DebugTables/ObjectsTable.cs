// <copyright file="ObjectsTable.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins.DebugTables
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Ensage.SDK.Extensions;
    using Ensage.SDK.Helpers;

    [Export(typeof(Table))]
    public sealed class ObjectsTable : Table
    {
        public ObjectsTable()
            : base("Units")
        {
        }

        internal override IReadOnlyList<TableColumn> OnUpdate()
        {
            var data = EntityManager<Unit>
                .Entities.Where(e => !e.UnitState.HasFlag(UnitState.FakeAlly) && e.MaximumHealth > 0)
                .OrderByDescending(e => e is Hero)
                .ThenBy(e => e.Team)
                .ToArray();

            return new[]
                   {
                       data.Select(e => e.Name).ToColumn("Name"),
                       data.Select(e => $"{e.Health:n0} / {e.MaximumHealth:n0}").ToColumn("Health"),
                       data.Select(e => $"{e.Mana:n0} / {e.MaximumMana:n0}").ToColumn("Mana"),
                       data.Select(e => e.Animation.Name).ToColumn("Animation", 200),
                       data.Select(e => e.UnitState.ToString()).ToColumn("State")
                   };
        }
    }
}