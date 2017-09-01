// <copyright file="Map.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;

    using Ensage.SDK.Geometry;
    using Ensage.SDK.Helpers.Metadata;

    using log4net;

    using PlaySharp.Toolkit.Helper;
    using PlaySharp.Toolkit.Logging;

    using SharpDX;

    [Flags]
    public enum MapArea
    {
        Unknown = 0,

        Top = (1 << 0),

        Middle = (1 << 1),

        Bottom = (1 << 2),

        River = (1 << 3),

        RadiantBase = (1 << 4),

        DireBase = (1 << 5),

        RoshanPit = (1 << 6),

        DireBottomJungle = (1 << 7),

        DireTopJungle = (1 << 8),

        RadiantBottomJungle = (1 << 9),

        RadiantTopJungle = (1 << 10),

        Jungle = DireBottomJungle | DireTopJungle | RadiantBottomJungle | RadiantTopJungle,
    }

    [ExportMap("start")]
    public class Map
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [ImportingConstructor]
        public Map()
        {
            try
            {
                // Map Polygons
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

                // Creep Routes
                this.RadiantTopRoute = this.LoadRoute("RadiantTopRoute");
                this.RadiantMiddleRoute = this.LoadRoute("RadiantMiddleRoute");
                this.RadiantBottomRoute = this.LoadRoute("RadiantBottomRoute");

                this.DireTopRoute = this.LoadRoute("DireTopRoute");
                this.DireMiddleRoute = this.LoadRoute("DireMiddleRoute");
                this.DireBottomRoute = this.LoadRoute("DireBottomRoute");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public List<Vector3> RadiantTopRoute { get; }

        public List<Vector3> RadiantMiddleRoute { get; }

        public List<Vector3> RadiantBottomRoute { get; }

        public List<Vector3> DireTopRoute { get; }

        public List<Vector3> DireMiddleRoute { get; }

        public List<Vector3> DireBottomRoute { get; }


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

        public MapArea GetLane(Unit unit)
        {
            var pos = unit.NetworkPosition;
            if (this.Top.IsInside(pos))
            {
                return MapArea.Top;
            }
            else if (this.Middle.IsInside(pos))
            {
                return MapArea.Middle;
            }
            else if (this.Bottom.IsInside(pos))
            {
                return MapArea.Bottom;
            }
            else if (this.River.IsInside(pos))
            {
                return MapArea.River;
            }
            else if (this.RadiantBase.IsInside(pos))
            {
                return MapArea.RadiantBase;
            }
            else if (this.DireBase.IsInside(pos))
            {
                return MapArea.DireBase;
            }
            else if (this.Roshan.IsInside(pos))
            {
                return MapArea.RoshanPit;
            }
            else if (this.DireBottomJungle.IsInside(pos) )
            {
                return MapArea.DireBottomJungle;
            }
            else if (this.DireTopJungle.IsInside(pos) )
            {
                return MapArea.DireTopJungle;
            }
            else if (this.RadiantBottomJungle.IsInside(pos) )
            {
                return MapArea.RadiantBottomJungle;
            }
            else if (this.RadiantTopJungle.IsInside(pos) )
            {
                return MapArea.RadiantTopJungle;
            }

            return MapArea.Unknown;
        }

        public List<Vector3> GetCreepRoute(Creep unit, MapArea lane = MapArea.Unknown)
        {
            var team = unit.Team;
            if (team != Team.Dire && team != Team.Radiant)
            {
                return new List<Vector3>();
            }

            if (lane == MapArea.Unknown)
            {
                lane = this.GetLane(unit);
            }

            if (lane == MapArea.Top)
            {
                return team == Team.Dire ? this.DireTopRoute : this.RadiantTopRoute;
            }
            else if (lane == MapArea.Middle)
            {
                return team == Team.Dire ? this.DireMiddleRoute : this.RadiantMiddleRoute;
            }
            else if (lane == MapArea.Bottom)
            {
                return team == Team.Dire ? this.DireBottomRoute : this.RadiantBottomRoute;
            }

            return new List<Vector3>();
        }

        private WorldPolygon Load(string name)
        {
            var data = JsonFactory.FromResource<Vector3[]>($"Resources.{name}.json", Assembly.GetExecutingAssembly());
            return new WorldPolygon(data);
        }

        private List<Vector3> LoadRoute(string name)
        {
            var data = JsonFactory.FromResource<Vector3[]>($"Resources.{name}.json", Assembly.GetExecutingAssembly());
            return data.ToList();
        }
    }
}