using System;
using System.Collections.Generic;
using System.Reflection;
using Collabo.Common;
using Collabo.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class CollaboJSONDataFileService: JSONDataFileService<CollaboDB>{
    public CollaboJSONDataFileService(string dbName):base(dbName, new ChannelConverter())
    {
        
    }
}

    public class ChannelConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(IChannel));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            if(jo["Type"].Value<int>() == 1){
                return jo.ToObject<NamedChannel>(serializer);
            }

            if(jo["Type"].Value<int>() == 2){
                return jo.ToObject<TemporaryChannel>(serializer);
            }
            return null;
        }


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject jo = new JObject();
            Type type = value.GetType();
            jo.Add("type", type.Name);

            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (prop.CanRead)
                {
                    object propVal = prop.GetValue(value, null);
                    if (propVal != null)
                    {
                        jo.Add(prop.Name, JToken.FromObject(propVal, serializer));
                    }
                }
            }
            jo.WriteTo(writer);
        }
    }
