// <copyright file="TableColumn.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    using SharpDX;

    public sealed class TableColumn
    {
        private readonly int minSize;

        public TableColumn(string name, IEnumerable<string> entries)
        {
            this.Name = name;
            this.Entries = entries.ToArray();
            this.ColumnSize = this.MeasureSize();
        }

        public TableColumn(string name, int minSize, IEnumerable<string> entries)
        {
            this.minSize = minSize;
            this.Name = name;
            this.Entries = entries.ToArray();
            this.ColumnSize = this.MeasureSize();
        }

        public float ColumnSize { get; }

        public string[] Entries { get; }

        public string Name { get; }

        private float MeasureSize()
        {
            var flag = FontFlags.DropShadow | FontFlags.AntiAlias;
            var size = new Vector2(22);
            var name = "Arial";
            var maxSize = this.minSize + Drawing.MeasureText(this.Name, name, size, flag)[0];

            foreach (var text in this.Entries)
            {
                var textSize = Drawing.MeasureText(text, name, size, flag);
                if (textSize[0] > maxSize)
                {
                    maxSize = textSize[0];
                }
            }

            return maxSize;
        }
    }
}