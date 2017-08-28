// <copyright file="Map.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.ComponentModel.Composition;
    using System.Reflection;

    using Ensage.SDK.Geometry;
    using Ensage.SDK.Helpers.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    [ExportMap("start")]
    public class Map
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ImportingConstructor]
        public Map()
        {
            try
            {
                this.Top = this.Load("Top");
                this.Middle = this.Load("Middle");
                this.Bottom = this.Load("Bottom");

                this.DireBase = this.Load("DireBase");
                this.RadiantBase = this.Load("RadiantBase");
                this.River = this.Load("River");
                this.Roshan = this.Load("Roshan");

                this.DireBottomJungle = this.Load("DireBottomJungle");
                this.DireTopJungle = this.Load("DireTopJungle");

                this.RadiantBottomJungle = this.Load("RadiantBottomJungle");
                this.RadiantTopJungle = this.Load("RadiantTopJungle");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public WorldPolygon Bottom { get; }

        public WorldPolygon DireBase { get; }

        public WorldPolygon DireBottomJungle { get; }

        public WorldPolygon DireTopJungle { get; }

        public WorldPolygon Middle { get; }

        public WorldPolygon RadiantBase { get; }

        public WorldPolygon RadiantBottomJungle { get; }

        public WorldPolygon RadiantTopJungle { get; }

        public WorldPolygon River { get; }

        public WorldPolygon Roshan { get; }

        public WorldPolygon Top { get; }

        private WorldPolygon Load(string name)
        {
            var data = JsonFactory.FromResource<Vector3[]>($"Resources.{name}.json", Assembly.GetExecutingAssembly());
            return new WorldPolygon(data);
        }
    }
}