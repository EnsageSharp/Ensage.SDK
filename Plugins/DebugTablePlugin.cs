// <copyright file="DebugTablePlugin.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Windows.Input;

    using Ensage.SDK.Helpers;
    using Ensage.SDK.Input;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    using SharpDX;

    [ExportPlugin("Debug Tables", StartupMode.Manual, priority: 1000, description: "Display Debug Tables")]
    public class DebugTablePlugin : Plugin, IPartImportsSatisfiedNotification
    {
        private const float ScrollSpeed = 20f;

        private readonly IServiceContext context;

        private readonly IInputManager input;

        [ImportingConstructor]
        public DebugTablePlugin([Import] IServiceContext context)
        {
            this.context = context;
            this.input = context.Input;
        }

        public DebugTableConfig Config { get; private set; }

        [ImportMany(typeof(Table), AllowRecomposition = true)]
        public IEnumerable<Table> Tables { get; private set; }

        private float OffsetX { get; set; } = 300f;

        private float OffsetY { get; set; } = 100f;

        public void OnImportsSatisfied()
        {
            this.Config?.Update(this.Tables.Select(e => e.Name));
        }

        protected override void OnActivate()
        {
            this.Config = new DebugTableConfig(this.context.Config.Factory);
            this.Config.Toggle.PropertyChanged += this.OnToggleChanged;
            this.OnImportsSatisfied();

            if (this.Config.Toggle)
            {
                this.Show();
            }
        }

        protected override void OnDeactivate()
        {
            this.Hide();
            this.Config.Toggle.PropertyChanged -= this.OnToggleChanged;
        }

        private void Hide()
        {
            Drawing.OnDraw -= this.OnDraw;
            UpdateManager.Unsubscribe(this.OnUpdate);

            this.input.UnregisterHotkey("MoveUp");
            this.input.UnregisterHotkey("MoveDown");
            this.input.UnregisterHotkey("MoveLeft");
            this.input.UnregisterHotkey("MoveRight");
        }

        private void MoveDown(KeyEventArgs args)
        {
            this.OffsetY += ScrollSpeed;
        }

        private void MoveLeft(KeyEventArgs args)
        {
            this.OffsetX -= ScrollSpeed;
        }

        private void MoveRight(KeyEventArgs args)
        {
            this.OffsetX += ScrollSpeed;
        }

        private void MoveUp(KeyEventArgs args)
        {
            this.OffsetY -= ScrollSpeed;
        }

        private void OnDraw(EventArgs args)
        {
            var flag = FontFlags.DropShadow | FontFlags.AntiAlias;
            var color = Color.Beige;
            var size = new Vector2(22);
            var name = "Arial";

            var tables = this.Tables.Where(e => this.Config.Factory.GetValue<bool>(e.Name) && e.HasEntries).OrderBy(e => e.Height).ToArray();
            var pos = new Vector2(this.OffsetX, this.OffsetY);
            var row = this.OffsetY;
            var width = tables.Length == 0 ? 0 : tables.Max(e => e.Width);
            var height = tables.Length == 0 ? 0 : tables.Sum(e => e.Height);

            Drawing.DrawRect(new Vector2(this.OffsetX - 10, this.OffsetY - 10), new Vector2(width, height), Color.Red, true);
            Drawing.DrawRect(new Vector2(this.OffsetX - 10, this.OffsetY - 10), new Vector2(width, height), new Color(0, 0, 0, 150));

            foreach (var table in tables)
            {
                foreach (var column in table.Columns)
                {
                    pos.Y = row;
                    Drawing.DrawText(column.Name, name, pos, size, Color.Red, flag);
                    pos.Y += 25;

                    foreach (var entry in column.Entries)
                    {
                        Drawing.DrawText(entry, name, pos, size, color, flag);
                        pos.Y += 25;
                    }

                    pos.X += column.ColumnSize + 20;
                }

                pos.X = this.OffsetX;
                pos.Y += 25;
                row = pos.Y;
            }
        }

        private void OnToggleChanged(object sender, PropertyChangedEventArgs args)
        {
            if (this.Config.Toggle)
            {
                this.Show();
            }
            else
            {
                this.Hide();
            }
        }

        private void OnUpdate()
        {
            foreach (var value in this.Tables)
            {
                value.OnUpdate();
            }
        }

        private void Show()
        {
            Drawing.OnDraw += this.OnDraw;
            UpdateManager.Subscribe(this.OnUpdate);

            this.input.RegisterHotkey("MoveUp", Key.NumPad8, this.MoveUp);
            this.input.RegisterHotkey("MoveDown", Key.NumPad2, this.MoveDown);
            this.input.RegisterHotkey("MoveLeft", Key.NumPad4, this.MoveLeft);
            this.input.RegisterHotkey("MoveRight", Key.NumPad6, this.MoveRight);
        }
    }
}