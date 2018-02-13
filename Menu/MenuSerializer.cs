// <copyright file="MenuSerializer.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.Menu
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json.Serialization;

    using NLog;

    using PlaySharp.Toolkit.Helper.Annotations;

    //public class ShouldSerializeContractResolver : DefaultContractResolver
    //{
    //    public static new readonly ShouldSerializeContractResolver Instance = new ShouldSerializeContractResolver();

    //    // protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    //    // {
    //    // var property = base.CreateProperty(member, memberSerialization);
    //    // var serialize = member.GetCustomAttribute<ItemAttribute>() != null || member.GetCustomAttribute<MenuAttribute>() != null;

    //    // bool PropertyShouldSerialize(object instance)
    //    // {
    //    // if (member.GetCustomAttribute<ItemAttribute>() != null || member.GetCustomAttribute<MenuAttribute>() != null)
    //    // {
    //    // return true;
    //    // }

    //    // return serialize;
    //    // }

    //    // property.ShouldSerialize = PropertyShouldSerialize;
    //    // return property;
    //    // }
    //    //protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    //    //{
    //    //    if (type.GetCustomAttribute<ItemAttribute>() != null)
    //    //    {
    //    //        return type.GetProperties()
    //    //                   .Select(
    //    //                       p => new JsonProperty()
    //    //                                {
    //    //                                    PropertyName = p.Name,
    //    //                                    PropertyType = p.PropertyType,
    //    //                                    Readable = true,
    //    //                                    Writable = true,
    //    //                                    ValueProvider = this.CreateMemberValueProvider(p)
    //    //                                })
    //    //                   .ToList();
    //    //    }

    //    //    var list = type.GetProperties()
    //    //                   .Where(x => x.GetCustomAttributes().Any(y => y.GetType() == typeof(MenuAttribute) || y.GetType() == typeof(ItemAttribute)))
    //    //                   .Select(
    //    //                       p => new JsonProperty()
    //    //                                {
    //    //                                    PropertyName = p.Name,
    //    //                                    PropertyType = p.PropertyType,
    //    //                                    Readable = true,
    //    //                                    Writable = true,
    //    //                                    ValueProvider = this.CreateMemberValueProvider(p)
    //    //                                })
    //    //                   .ToList();

    //    //    return list;
    //    //}

    //    //protected override List<MemberInfo> GetSerializableMembers(Type objectType)
    //    //{
    //    //    var members = base.GetSerializableMembers(objectType);
    //    //    members.RemoveAll(x => x.GetCustomAttribute<MenuAttribute>() == null && x.GetCustomAttribute<ItemAttribute>() == null);
    //    //    return members;
    //    //}
    //}

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
                                    //,ContractResolver = new ShouldSerializeContractResolver()
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