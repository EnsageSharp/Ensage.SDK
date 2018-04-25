// <copyright file="MenuSerializer.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;

    using Ensage.SDK.Menu.Attributes;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using NLog;

    using PlaySharp.Toolkit.Helper.Annotations;

    public class MenuSerializer
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public MenuSerializer(params JsonConverter[] converters)
        {
            this.Settings = new JsonSerializerSettings
                                {
                                    Formatting = Formatting.Indented,
                                    DefaultValueHandling = DefaultValueHandling.Include | DefaultValueHandling.Populate,
                                    NullValueHandling = NullValueHandling.Ignore,
                                    TypeNameHandling = TypeNameHandling.Auto,
                                    Converters = converters,
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
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
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                using (var writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartObject();

                    this.Serialize(writer, dataContext);

                    writer.WriteEndObject();
                }
            }

            var type = dataContext.GetType();
            var assemblyName = type.Assembly.GetName().Name;
            var dir = Path.Combine(this.ConfigDirectory, assemblyName);
            var file = Path.Combine(dir, $"{type.FullName}.json");

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(file, sb.ToString());
        }

        public object ToObject(JToken token, Type type)
        {
            return token.ToObject(type, this.JsonSerializer);
        }

        private void Serialize(JsonWriter writer, object context)
        {
            var type = context.GetType();
            var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            // save menus
            foreach (var propertyInfo in propertyInfos)
            {
                var propertyValue = propertyInfo.GetValue(context);

                var menuAttribute = propertyInfo.GetCustomAttribute<MenuAttribute>();
                if (menuAttribute != null)
                {
                    writer.WritePropertyName(propertyValue.GetType().Name);
                    writer.WriteStartObject();
                    this.Serialize(writer, propertyValue);
                    writer.WriteEndObject();
                    continue;
                }

                var itemAttribute = propertyInfo.GetCustomAttribute<ItemAttribute>();
                if (itemAttribute != null)
                {
                    writer.WritePropertyName(propertyInfo.Name);
                    this.WritePropertyValue(writer, propertyValue);
                    continue;
                }

                var saveAttribute = propertyInfo.GetCustomAttribute<ConfigSaveAttribute>();
                if (saveAttribute != null)
                {
                    writer.WritePropertyName(propertyInfo.Name);
                    this.WritePropertyValue(writer, propertyValue);
                    continue;
                }
            }
        }

        // public void Serialize(object dataContext)
        // {
        // var type = dataContext.GetType();
        // var assemblyName = type.Assembly.GetName().Name;
        // var dir = Path.Combine(this.ConfigDirectory, assemblyName);
        // var file = Path.Combine(dir, $"{type.FullName}.json");
        // var json = JsonConvert.SerializeObject(dataContext, this.Settings);

        // if (!Directory.Exists(dir))
        // {
        // Directory.CreateDirectory(dir);
        // }

        // File.WriteAllText(file, json);
        // }
        private void WritePropertyValue(JsonWriter writer, object propertyValue)
        {
            var propertyType = propertyValue.GetType();
            if (propertyType.IsArray && propertyValue is object[] values)
            {
                writer.WriteStartArray();
                foreach (var value in values)
                {
                    this.JsonSerializer.Serialize(writer, propertyValue);
                }

                writer.WriteEnd();
            }
            else
            {
                this.JsonSerializer.Serialize(writer, propertyValue);
            }
        }
    }
}