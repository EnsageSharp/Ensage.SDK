namespace Ensage.SDK.Menu
{
    using System;
    using System.IO;
    using System.Reflection;

    using log4net;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using PlaySharp.Toolkit.Helper.Annotations;
    using PlaySharp.Toolkit.Logging;

    public class MenuSerializer
    {
        private static readonly ILog Log = AssemblyLogs.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public MenuSerializer(params JsonConverter[] converters)
        {
            this.Settings = new JsonSerializerSettings
                                {
                                    Formatting = Formatting.Indented,
                                    DefaultValueHandling = DefaultValueHandling.Include | DefaultValueHandling.Populate,
                                    NullValueHandling = NullValueHandling.Ignore,
                                    TypeNameHandling = TypeNameHandling.Auto,
                                    Converters = converters
                                };

            this.JsonSerializer = JsonSerializer.Create(this.Settings);
            this.ConfigDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "game");
            try
            {
                Directory.CreateDirectory(this.ConfigDirectory);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public string ConfigDirectory { get; }

        public JsonSerializer JsonSerializer { get; }

        public JsonSerializerSettings Settings { get; }

        [CanBeNull]
        public JToken Deserialize(object dataContext)
        {
            var type = dataContext.GetType();
            var assemblyName = type.Assembly.GetName().Name;
            var dir = Path.Combine(this.ConfigDirectory, assemblyName);
            var file = Path.Combine(dir, $"{type.FullName}.json");

            if (!File.Exists(file))
            {
                return null;
            }

            try
            {
                var json = File.ReadAllText(file);
                return JToken.Parse(json);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }

        public void Serialize(object dataContext)
        {
            var type = dataContext.GetType();
            var assemblyName = type.Assembly.GetName().Name;
            var dir = Path.Combine(this.ConfigDirectory, assemblyName);
            var file = Path.Combine(dir, $"{type.FullName}.json");
            var json = JsonConvert.SerializeObject(dataContext, this.Settings);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(file, json);
        }

        public object ToObject(JToken token, Type type)
        {
            return token.ToObject(type, this.JsonSerializer);
        }
    }
}