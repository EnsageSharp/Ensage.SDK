namespace Ensage.SDK.Menu.Styles
{
    using System;
    using System.Linq;
    using System.Reflection;

    

    using Newtonsoft.Json;

    using PlaySharp.Toolkit.Helper;
    using NLog;

    public class MenuStyleConverter : JsonConverter
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsGenericTypeDefinition)
            {
                Log.Error($"CAN CONVERT BLYAT {objectType.GetGenericTypeDefinition()} == {typeof(IMenuStyle)}");
                return objectType.GetGenericTypeDefinition() == typeof(IMenuStyle);
            }

            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var name = reader.ReadAsString();
            Log.Error($"NAME {name}");
            return IoC.Get<StyleRepository>().Styles.FirstOrDefault(x => x.Name == name);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Log.Error($"WRITING {((IMenuStyle)value).Name}");
            writer.WriteValue(((IMenuStyle)value).Name);
        }
    }
}