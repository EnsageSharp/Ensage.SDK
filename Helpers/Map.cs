// <copyright file="Map.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.ComponentModel.Composition;
    using System.Reflection;

    using Ensage.SDK.Geometry;

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
                var top = JsonFactory.FromResource<Vector3[]>("Resources.top.json", Assembly.GetExecutingAssembly());
                var mid = JsonFactory.FromResource<Vector3[]>("Resources.mid.json", Assembly.GetExecutingAssembly());
                var bot = JsonFactory.FromResource<Vector3[]>("Resources.bot.json", Assembly.GetExecutingAssembly());

                this.Top = new WorldPolygon(top);
                this.Mid = new WorldPolygon(mid);
                this.Bottom = new WorldPolygon(bot);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public WorldPolygon Bottom { get; }

        public WorldPolygon Mid { get; }

        public WorldPolygon Top { get; }
    }

    public interface IMapMetadata
    {
        string Name { get; }
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportMapAttribute : ExportAttribute, IMapMetadata
    {
        public ExportMapAttribute(string name)
            : base(typeof(Map))
        {
            this.Name = name;
        }

        public string Name { get; }
    }
}