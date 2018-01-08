// <copyright file="MenuStyleConverter.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;
    using System.Linq;

    using Ensage.SDK.Menu.Styles;
    using Ensage.SDK.Menu.Styles.Elements;

    using Newtonsoft.Json;

    public class MenuStyleConverter : JsonConverter
    {
        private readonly StyleRepository repository;

        private readonly Type Type = typeof(IMenuStyle);

        public MenuStyleConverter(StyleRepository repository)
        {
            this.repository = repository;
        }

        public override bool CanConvert(Type objectType)
        {
            return this.Type.IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var name = (string)reader.Value;
            return this.repository.Styles.FirstOrDefault(x => x.Name == name);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var menuStyle = (IMenuStyle)value;
            writer.WriteValue(menuStyle.Name);
        }
    }
}