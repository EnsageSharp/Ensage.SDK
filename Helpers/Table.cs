// <copyright file="Table.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Table
    {
        protected Table(string name)
        {
            this.Name = name;
        }

        public IReadOnlyList<TableColumn> Columns { get; protected set; } = new TableColumn[0];

        public bool HasEntries
        {
            get
            {
                return this.Columns.Count > 0 && this.Columns.Any(e => e.Entries.Length > 0);
            }
        }

        public float Height
        {
            get
            {
                if (!this.HasEntries)
                {
                    return 0;
                }

                return 50f + (this.Columns.Max(e => e.Entries.Length) * 25f);
            }
        }

        public string Name { get; }

        public float Width
        {
            get
            {
                return this.Columns.Sum(e => e.ColumnSize + 20);
            }
        }

        internal abstract void OnUpdate();
    }
}