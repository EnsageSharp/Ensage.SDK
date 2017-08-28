// <copyright file="PolygonRecorder.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Samples
{
    using System;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Windows.Input;

    using Ensage.SDK.Geometry;
    using Ensage.SDK.Service;
    using Ensage.SDK.Service.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    using KeyEventArgs = Ensage.SDK.Input.KeyEventArgs;
    using MouseEventArgs = Ensage.SDK.Input.MouseEventArgs;

    [ExportPlugin("Polygon recorder", StartupMode.Manual)]
    public class PolygonRecorder : Plugin
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IServiceContext context;

        [ImportingConstructor]
        public PolygonRecorder([Import] IServiceContext context)
        {
            this.context = context;
        }

        public WorldPolygon Polygon { get; } = new WorldPolygon();

        protected override void OnActivate()
        {
            Drawing.OnDraw += this.OnDraw;
            this.context.Input.MouseClick += this.OnMouseClick;
            this.context.Input.RegisterHotkey("save", Key.NumPad0, this.SavePolygon);
        }

        protected override void OnDeactivate()
        {
            Drawing.OnDraw -= this.OnDraw;
            this.context.Input.MouseClick -= this.OnMouseClick;
        }

        private void Add()
        {
            var pos = Game.MousePosition;

            if (this.Polygon.Points.Contains(pos))
            {
                return;
            }

            Log.Debug($"Add {pos}");
            this.Polygon.Add(pos);
        }

        private void OnDraw(EventArgs args)
        {
            this.Polygon.Draw(Color.Yellow);
        }

        private void OnMouseClick(object sender, MouseEventArgs args)
        {
            switch (args.Buttons)
            {
                case MouseButtons.Left:
                    this.Add();
                    break;

                case MouseButtons.Right:
                    this.Remove();
                    break;
            }
        }

        private void Remove()
        {
            var pos = Game.MousePosition;
            this.Polygon.Points.RemoveAll(point => pos.Distance(point) < 100);
        }

        private void SavePolygon(KeyEventArgs obj)
        {
            JsonFactory.ToFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "game", "polygon.json"), this.Polygon.Points);
        }
    }
}